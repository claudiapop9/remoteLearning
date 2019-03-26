using System;
using System.Collections.Generic;

namespace VendingMachineCodeFirst
{

    class UI
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Controller ctrl = new Controller();
        IPayment payment;
        
        public void Run()
        {
            MainMenu();
        }

        public void MainMenu()
        {
            string str = "------------Menu----------------\n\n";
            str += "1.List of products\n";
            str += "2.Buy product\n";
            str += "3.Modify product\n";
            Console.WriteLine(str);
            string c = Console.ReadLine();
            switch (c)
            {
                case "1":
                    ShowProductList();
                    break;
                case "2":
                    WayOfPayment();
                    break;
                case "3":
                    ModifyProductList();
                    break;
            }
        }
        public void ShowProductList()
        {
            List<Product> products = ctrl.GetProductsList();

            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine(products[i]);
            }

            Console.ReadKey();
        }

        public void WayOfPayment()
        {
            string str = "-----------Payment----------------\n\n";
            str += "1.Cash\n";
            str += "2.By Card\n";
            Console.WriteLine(str);
            string c = Console.ReadLine();
            switch (c)
            {
                case "1":
                    log.Info("Cash Payment");
                    payment = new CashPayment();
                    ShopMenu();
                    break;
                case "2":
                    log.Info("Card Payment");
                    payment = new CardPayment();
                    ShopMenu();
                    break;
            }
        }
        public void ShopMenu()
        {
            ShowProductList();
            try
            {
                Console.WriteLine("Product id:");
                int id = Int32.Parse(Console.ReadLine());
                ctrl = new Controller(payment);
                if (ctrl.BuyProduct(id))
                {
                    Console.WriteLine("Product bought successfully :D");
                    ShowProductList();
                    log.Info("Product bought successfully");
                }
                else
                {
                    Console.WriteLine("The Product wasn't bought :( \n");
                    Console.ReadKey();
                    log.Info("The Product wasn't bought :( \n");
                }
            }
            catch (Exception e) {
                log.Error(e);
 
            }
            
        }
        public void ModifyProductList()
        {
            Tuple<string, int, double> tuple;
            Tuple<string, int, double> emptyTuple= new Tuple<string, int, double>("", -1, -1);
            Console.WriteLine(" Press 1 to add product\n Press 2 to update product\n Press 3 to delete product");
            string cmd = Console.ReadLine();
            switch (cmd)
            {
                case "1":
                    log.Info("ADD product ");
                    tuple = AskDetails();
                    if (tuple != emptyTuple) {
                        ctrl.AddProductToList(tuple.Item1, tuple.Item2, tuple.Item3);
                        ShowProductList();
                    }
                    break;
                case "2":
                    log.Info("UPDATE product ");
                    Console.WriteLine("Id:");
                    int id = Int32.Parse(Console.ReadLine());
                    tuple = AskDetails();
                    if (tuple != emptyTuple)
                    {
                        ctrl.UpdateProductInList(id, tuple.Item1, tuple.Item2, tuple.Item3);
                        ShowProductList();
                    }
                    break;
                case "3":
                    log.Info("DELETE product ");
                    Console.WriteLine("Introduce id:");
                    try
                    {
                        int id2 = Int32.Parse(Console.ReadLine());
                        ctrl.DeleteProductFromList(id2);
                        ShowProductList();
                    }
                    catch (Exception) {
                        log.Error("Id must be integer!");
                    }
                    break;
            }
        }

        public Tuple<string, int, double> AskDetails()
        {
            try
            {
                Console.WriteLine("Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Quantity:");
                int quantity = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Price:");
                double price = Double.Parse(Console.ReadLine());

                return new Tuple<string, int, double>(name, quantity, price);
            }
            catch (Exception) {
                log.Error("PRODUCT Details incorrect introduced");
            }
            return new Tuple<string, int, double>("",-1,-1);


        }
    }
}
