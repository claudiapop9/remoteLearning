using System;
using System.Text.RegularExpressions;

namespace VendingMachineAdmin
{
    class UI
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Controller ctrl=new Controller();

        public void Run()
        {
            MainMenu();
        }

        public void MainMenu()
        {
            string str = "------------Menu----------------\n\n";
            str += "1.Modify product\n";
            str += "2.Refill\n";
            str += "3.Generate report\n";

            Console.WriteLine(str);
            string c = Console.ReadLine();
            switch (c)
            {
                case "1":
                    ModifyProductList();
                    break;
                case "2":
                    RefillProducts();
                    break;
                case "3":
                    GenerateReport();
                    break;
            }
        }

        public void ModifyProductList()
        {
            Tuple<string, int, double> tuple;
            Tuple<string, int, double> emptyTuple = new Tuple<string, int, double>("", -1, -1);
            Console.WriteLine(" Press 1 to add product\n Press 2 to update product\n Press 3 to delete product");
            string cmd = Console.ReadLine();
            switch (cmd)
            {
                case "1":
                    log.Info("ADD product ");
                    tuple = AskDetails();
                    if (!Equals(tuple, emptyTuple))
                    {
                        string response = ctrl.AddProductToList(tuple.Item1, tuple.Item2, tuple.Item3);
                        Console.WriteLine(response);
                        log.Info(response);
                    }
                    break;
                case "2":
                    log.Info("UPDATE product ");
                    Console.WriteLine("Id:");
                    int id = Int32.Parse(Console.ReadLine());
                    tuple = AskDetails();
                    if (tuple != emptyTuple)
                    {

                        string response=ctrl.UpdateProductInList(id, tuple.Item1, tuple.Item2, tuple.Item3);
                        Console.WriteLine(response);
                    }
                    break;
                case "3":
                    
                    Console.WriteLine("Introduce id:");
                    try
                    {
                        int id2 = Int32.Parse(Console.ReadLine());
                        string response=ctrl.DeleteProductFromList(id2);
                        log.Info("DELETE product "+ id2);
                        Console.WriteLine(response);
                       
                    }
                    catch (Exception)
                    {
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

                Regex regexPrice= new Regex(@"^\d+([.]\d+)?$");
                Regex regexQuantity = new Regex(@"^([1-9]+\d*)$|^0$");

                if (regexPrice.IsMatch(price.ToString()) && regexQuantity.IsMatch(quantity.ToString()))
                {
                   
                    return new Tuple<string, int, double>(name, quantity, price);
                }
                
            }
            catch (Exception)
            {
                log.Error("PRODUCT Details incorrect introduced");
            }
            return new Tuple<string, int, double>("", -1, -1);
        }

        private void RefillProducts()
        {
            Console.WriteLine(ctrl.Refill());
        }

        private void GenerateReport()
        {
           string response=ctrl.GenerateReport();
           Console.WriteLine(response);
        }

    }
    }
