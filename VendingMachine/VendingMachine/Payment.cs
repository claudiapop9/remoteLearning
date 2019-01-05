
namespace VendingMachine
{
    class Payment
    {
        double money;

        public Payment(double money)
        {
            this.money = money;
        }

        public bool IsEnough(double cost)
        {
            return cost.Equals(money);
        }

        
    }
}
