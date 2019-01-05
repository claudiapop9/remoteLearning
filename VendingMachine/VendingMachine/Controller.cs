using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Controller
    {

        private ProductCollection productCollection = new ProductCollection();
        private string filename;
               
        public Controller(string filename)
        {
            this.filename = filename;
            ReadFromFile(filename);
        }
        

        public void ReadFromFile(string filename)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                string[] copyLine=line.Split(' ');
                
                int productId = Int32.Parse(copyLine[0]);
                string name = copyLine[1];
                
                int quantity = Int32.Parse(copyLine[2]);
                double price = Double.Parse(copyLine[3]);
                Product p = new Product(name, quantity, price);
                productCollection.AddProduct(productId,p);
            }
            file.Close();
        }

             

        public bool BuyProduct(int productId, double introducedMoney)
        {
            double productPrice = productCollection.GetProductByKey(productId).PriceProperty;
            Payment payment = new Payment(introducedMoney);
            if (payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(productId);
                return true;
            }
            else { return false; }
        }

        public void AddProductToList(int productId, string name, int quantity, double price)
        {
            Product product = new Product(name, quantity, price);
            this.productCollection.AddProduct(productId, product);
            WriteToFile(filename);
        }

        public void UpdateProductInList(int productId, string name, int quantity, double price) {
            this.productCollection.UpdateProduct(productId, name, quantity, price);
            WriteToFile(filename);
        }

        public void DeleteProductFromList(int productId)
        {
            this.productCollection.RemoveProduct(productId);
            WriteToFile(filename);
        }

        public void WriteToFile(string filename)
        {
            Dictionary<int, Product> products = this.productCollection.GetProducts();
            string text = "";
            foreach (int id in products.Keys)
            {
                text += id + " " + products[id].NameProperty + " " + products[id].QuantityProperty + " " + products[id].PriceProperty + "\n";
            }
            System.IO.File.WriteAllText(filename, text);
        }

        public ProductCollection GetProductCollection()
        {
            return productCollection;
        }

    }
}
