using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class UI
    {
        Controller ctrl = new Controller();

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
                    ShopMenu();
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

        public void ShopMenu()
        {
            ShowProductList();
            Console.WriteLine("Product id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Introduce money:");
            double introducedMoney = Double.Parse(Console.ReadLine());
            if (ctrl.BuyProduct(id, introducedMoney))
            {
                Console.WriteLine("Product bought successfully :D");
                ShowProductList();
            }
            else
            {
                Console.WriteLine("Not enought money :( \n");
                Console.ReadKey();
            }

        }

        public void ModifyProductList()
        {
            Tuple<string, int, double> tuple;
            Console.WriteLine(" Press 1 to add product\n Press 2 to update product\n Press 3 to delete product");
            string cmd = Console.ReadLine();
            switch (cmd)
            {
                case "1":
                    tuple = AskDetails();
                    ctrl.AddProductToList(tuple.Item1, tuple.Item2, tuple.Item3);
                    ShowProductList();
                    break;
                case "2":
                    Console.WriteLine("Id:");
                    int id = Int32.Parse(Console.ReadLine());
                    tuple = AskDetails();
                    ctrl.UpdateProductInList(id, tuple.Item1, tuple.Item2, tuple.Item3);
                    ShowProductList();
                    break;
                case "3":
                    Console.WriteLine("Introduce id:");
                    int id2 = Int32.Parse(Console.ReadLine());
                    ctrl.DeleteProductFromList(id2);
                    ShowProductList();
                    break;
            }

        }

        public Tuple<string, int, double> AskDetails()
        {

            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Quantity:");
            int quantity = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Price:");
            double price = Double.Parse(Console.ReadLine());

            return new Tuple<string, int, double>(name, quantity, price);
        }
    }
}
