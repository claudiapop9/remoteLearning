
namespace VendingMachine
{
    class CreditCard : IPayment
    {
        private string cardNumber;
        private int cardPin;
        private double availableMoney;
        

        //just for the moment to test the program
        public CreditCard(double sum)
        {
            this.availableMoney = sum;
        }

        public bool IsEnough(double cost)
        {
            return (cost.Equals(availableMoney) || cost < availableMoney);
        }

        public bool IsValidPin(int pin) {

            return cardPin.Equals(pin);
        }

        public void TakeMoney(double cost)
        {
            availableMoney -= cost;
        }
    }
}
