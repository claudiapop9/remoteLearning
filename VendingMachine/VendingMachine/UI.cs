using System;


namespace VendingMachine
{
    class UI
    {
        Controller ctrl;
        string file = @"C:\Users\computer\source\repos\VendingMachine\VendingMachine\products.txt";

        public void Run()
        {
            MainMenu();

        }

        public void MainMenu()
        {
            string str = "------------Menu----------------\n\n";
            str += "1.List of products\n";
            str += "2.Buy product\n";
            Console.WriteLine(str);
            string c = Console.ReadLine();
            switch (c)
            {
                case "1":
                    ctrl = new Controller(file);
                    //Console.WriteLine(ctrl.GetProductCollection().ToString());
                    break;
                case "2":
                    ShopMenu();
                    break;
            }
        }

        public void ShopMenu()
        {
            Console.WriteLine("Product id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Method of payment : 1.Cash or 2.Credit card");
            string i = Console.ReadLine();

            switch (i)
            {

                case "1":
                    IPayment cash = new Cash(150);
                    ctrl = new Controller(file, cash);
                    Console.WriteLine(ctrl.GetProductCollection().ToString());
                    break;
                case "2":
                    IPayment card = new CreditCard(150);
                    ctrl = new Controller(file, card);
                    Console.WriteLine(ctrl.GetProductCollection().ToString());
                    break;

            }
        }
    }
}
