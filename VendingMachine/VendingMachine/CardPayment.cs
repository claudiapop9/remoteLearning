using System.Data.Entity;
using System.Linq;
using System;

namespace VendingMachine
{
    class CardPayment : IPayment
    {
        private string cardNo;
        private string pin;

        public CardPayment(string cardNo, string pin)
        {
            this.cardNo = cardNo;
            this.pin = pin;
        }

        public void Pay(double cost)
        {
            if (IsValidCard() && IsEnough(cost))
            {
                using (VMachineEntities db = new VMachineEntities())
                {
                    Bank bankAccount = db.Banks.Where(x => x.CardNO == cardNo).FirstOrDefault();
                    bankAccount.Amount -= cost;
                    db.Entry(bankAccount).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                if (!IsValidCard()) { throw new Exception("Not Valid Credentials"); }
                else if (!IsEnough(cost))
                {
                    throw new Exception("Not EnoughMoney");
                }
            }
        }
        public bool IsEnough(double cost)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                Bank bankAccount = db.Banks.Where(x => x.CardNO == cardNo).FirstOrDefault();
                if (bankAccount.Amount >= cost)
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsValidCard()
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                Bank bankAccount = db.Banks.Where(x => x.CardNO == cardNo).FirstOrDefault();
                if (bankAccount.Pin == pin)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
