using System.Data.Entity;
using System.Linq;
using System;

namespace VendingMachine
{
    class CardPayment : IPayment
    {
        private string cardNo;
        private string pin;
        private bool enough = false;
                
        public void Pay(double cost)
        {
            if (enough)
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
                throw new Exception("Not EnoughMoney");
            }
        }
        public bool IsEnough(double cost)
        {
            AskDetails();
            using (VMachineEntities db = new VMachineEntities())
            {
                Bank bankAccount = db.Banks.Where(x => x.CardNO == cardNo).FirstOrDefault();
                if (bankAccount.Amount >= cost)
                {
                    enough = true;
                    return enough;
                }
                return enough;
            }
        }
        public void AskDetails()
        {
            Console.WriteLine("CardNo:");
            string cardNo = Console.ReadLine();
            Console.WriteLine("PIN:");
            string pin = Console.ReadLine();
            if (IsValidCard(cardNo, pin))
            {
                this.cardNo = cardNo;
                this.pin = pin;
            }
            else
            {
                throw new Exception("Not Valid Credentials");
            }
        }

        public bool IsValidCard(string cardNumber, string cardPin)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                Bank bankAccount = db.Banks.Where(x => x.CardNO == cardNumber).FirstOrDefault();
                if (bankAccount !=null && bankAccount.Pin == cardPin)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
