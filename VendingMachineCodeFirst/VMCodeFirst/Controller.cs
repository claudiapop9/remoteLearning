using System.Collections.Generic;

namespace VendingMachineCodeFirst {
    class Controller
    {
        private List<CashMoney> introducedMoney = new List<CashMoney>();
        private ProductCollection productCollection = new ProductCollection();
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
        }

        public void UpdateProductInList(int productId, string name, int quantity, double price)
        {
            Product p = new Product(productId, name, quantity, price);
            this.productCollection.UpdateProduct(p);
        }

        public void DeleteProductFromList(int productId)
        {
            this.productCollection.RemoveProduct(productId);
        }

        public List<Product> GetProductsList()
        {
            return productCollection.GetProducts();
        }

        public bool BuyProduct(int productId)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            if (payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(productId);
                payment.Pay(productPrice);
                return true;
            }
            return false;
        }


    }
}
