namespace VendingMachine
{
    interface IPayment
    {
        void Pay(double cost);
        bool IsEnough(double cost);
    }
}
