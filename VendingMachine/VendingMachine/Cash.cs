
namespace VendingMachine
{
    class Cash : IPayment
    {
        private double sum = 0;

        public Cash(double sum)
        {
            this.sum = sum;
        }

        public void AddCoin(double value)
        {

            this.sum += value;
        }

        public bool IsEnough(double cost)
        {
            return (cost.Equals(sum) || cost < sum);
        }

        public void TakeMoney(double cost)
        {
            sum -= cost;
        }
    }
}
