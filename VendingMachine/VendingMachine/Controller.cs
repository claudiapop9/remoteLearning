using System;


namespace VendingMachine
{
    class Controller
    {

        private ProductCollection productCollection = new ProductCollection();
        private IPayment payment;
        private double vendingMachineMoney = 0;

        public Controller(string filename)
        {
            ReadFromFile(filename);

        }
        public Controller(string filename, IPayment payment)
        {
            ReadFromFile(filename);
            this.payment = payment;
        }

        public void ReadFromFile(string filename)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                line.Split(' ');
                int productId = line[0];
                string name = line[1].ToString();
                int quantity = line[2];
                double price = line[3];
                Product p = new Product(productId, name, quantity, price);
                productCollection.AddProduct(p);
            }
            Console.Write(this.productCollection);
            Console.ReadKey();
            file.Close();
        }

        public void BuyProduct(int id)
        {

            double productPrice = productCollection.GetProductByKey(id).PriceProperty;
            if (payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(id);
                payment.TakeMoney(productPrice);
            }
        }

        public ProductCollection GetProductCollection()
        {
            return productCollection;
        }




    }
}
