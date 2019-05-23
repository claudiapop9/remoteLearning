using System;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BusinessLogic
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Controller ctrl=new Controller();
            string products=ctrl.GetProducts();
            Console.WriteLine(products);
            Console.ReadKey();
        }
    }
}
