
namespace VendingMachine
{
    interface IPayment
    {
        bool IsEnough(double cost);
        void TakeMoney(double cost);
    }
}
