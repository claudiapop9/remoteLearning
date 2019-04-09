using System;
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
        private TransactionManager transactionManager=new TransactionManager();
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
        }

        public void DeleteProductFromList(int productId)
        {
            this.productCollection.RemoveProduct(productId);
            dataStorage.PersistData(this.productCollection.GetProducts());
        }
        public bool Refill()
        {
            List<Product> productsToRefList = productCollection.GetProductsToRefill();
            if (productCollection.Refill())
            {
                dataStorage.PersistData(this.productCollection.GetProducts());
                AddTransactionRefill(productsToRefList);
                return true;
            }
            else
            {
                return false;
            }

        }
        public void AddTransactionRefill(List<Product> products)
        {
            foreach (var prod in products)
            {
                AddTransition("REFILL", prod.ProductId);
            }
        }

        public void AddTransition(string info, int productId)
        {
            Transaction transaction = new Transaction(info, productId);
            transactionManager.AddTransaction(transaction);
        }

        public bool BuyProduct(int productId)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            if (productPrice !=-1 && payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(productId);
                payment.Pay(productPrice);
                dataStorage.PersistData(this.productCollection.GetProducts());
                AddTransition("BUY",productId);
                return true;
            }
            return false;
        }


        public void GenerateReport()
        {
            List<Transaction> transactions = transactionManager.GetTransactions();
            report.GenerateReport(reportPath,transactions);
        }

        public List<Product> GetProductsList()
        {
            return productCollection.GetProducts();
        }




    }
}
