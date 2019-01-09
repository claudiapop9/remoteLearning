using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class CardPayment : IPayment
    {
        public bool IsEnough(double cost)
        {
            throw new NotImplementedException();
        }

        public void Pay(double cost, double money)
        {
            throw new NotImplementedException();
        }

        private bool IsValid(string pin)
        {
            throw new NotImplementedException();
        }
    }
}
