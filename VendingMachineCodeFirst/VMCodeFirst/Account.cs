using Newtonsoft.Json;

namespace VendingMachineCodeFirst
{
   
    public class Account
    {
        public int AccountId { get; set; }
        public string CardNO { get; set; }
        public string Pin { get; set; }
        public double Amount { get; set; }

        public Account()
        { }

        [JsonConstructor]
        public Account(string CardNo, string Pin)
        {
            this.CardNO = CardNo;
            this.Pin = Pin;
        }
    }
}
