using System.Collections.Generic;

namespace VendingMachineCodeFirst {
    class Controller
    {
        private const string filePath = @".\currentDb.txt";
        private const string filePathAllStates = @".\all.txt";
        private const string reportPath = @".\report.txt";
        private List<CashMoney> introducedMoney = new List<CashMoney>();
        private ProductCollection productCollection = new ProductCollection();
        private Data dataStorage=new Data(filePath,filePathAllStates);
        private Report report = new Report();
        private IPayment payment;


        public Controller() { }
        public Controller(IPayment paymentMethod)
        {
            this.payment = paymentMethod;
        }

        public void AddProductToList(string name, int quantity, double price)
        {
            Product p = new Product(name, quantity, price);
            this.productCollection.AddProduct(p);
            dataStorage.PersistData(this.productCollection.GetProducts());
        }

        public void UpdateProductInList(int productId, string name, int quantity, double price)
        {
            Product p = new Product(productId, name, quantity, price);
            this.productCollection.UpdateProduct(p);
            //dataStorage.PersistData(this.productCollection.GetProducts());
        }

        public void DeleteProductFromList(int productId)
        {
            this.productCollection.RemoveProduct(productId);
            dataStorage.PersistData(this.productCollection.GetProducts());
        }
        public bool Refill()
        {
            if (productCollection.Refill())
            {
                dataStorage.PersistData(this.productCollection.GetProducts());
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool BuyProduct(int productId)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            if (productPrice !=-1 && payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(productId);
                payment.Pay(productPrice);
                dataStorage.PersistData(this.productCollection.GetProducts());
                return true;
            }
            return false;
        }

        public void GenerateReport()
        {
            report.GenerateReport(reportPath,dataStorage.GetAllStates());
        }

        public List<Product> GetProductsList()
        {
            return productCollection.GetProducts();
        }




    }
}
