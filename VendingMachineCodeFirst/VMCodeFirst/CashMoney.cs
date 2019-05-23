using Newtonsoft.Json;

namespace VendingMachineCodeFirst
{
        
    public class CashMoney
    {
        public int Id { get; set; }
        public double MoneyValue { get; set; }
        public int Quantity { get; set; }

        public CashMoney() { }

        [JsonConstructor]
        public CashMoney(double moneyValue, int quantity)
        {
            MoneyValue = moneyValue;
            Quantity = quantity;
        }
    }
}
