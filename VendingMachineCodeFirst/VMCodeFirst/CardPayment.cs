using System.Data.Entity;
using System.Linq;
using System;

namespace VendingMachineCodeFirst
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
                using (var db = new VendMachineDbContext())
                {
                    Account AccountAccount = db.Accounts.Where(x => x.CardNO == cardNo).FirstOrDefault();
                    AccountAccount.Amount -= cost;
                    db.Entry(AccountAccount).State = EntityState.Modified;
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
            using (var db = new VendMachineDbContext())
            {
                Account AccountAccount = db.Accounts.Where(x => x.CardNO == cardNo).FirstOrDefault();
                if (AccountAccount.Amount >= cost)
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
            using (var db = new VendMachineDbContext())
            {
                Account AccountAccount = db.Accounts.Where(x => x.CardNO == cardNumber).FirstOrDefault();
                if (AccountAccount !=null && AccountAccount.Pin == cardPin)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
