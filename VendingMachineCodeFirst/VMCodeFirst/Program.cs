using System;
using System.Collections.Generic;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace VendingMachineCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CashMoney> introducedMoney = new List<CashMoney>();
            introducedMoney.Add(new CashMoney(1,5));
            IPayment payment = new CashService(introducedMoney);
            Repository repository = new Repository(payment);
            List<Product> products = repository.GetProductsList();
            foreach (var prod in products)
            {
                Console.WriteLine(prod);
            }

            Console.ReadKey();
            repository.BuyProduct(1);
            Console.ReadKey();
        }
    }
}