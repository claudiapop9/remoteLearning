using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Controller
    {
        private Dictionary<double, int> introducedMoney = new Dictionary<double, int>() { { 0.5, 0 }, { 1, 0 }, { 5, 0 }, { 10, 0 } };

        private double totalMoney = 0;

        private ProductCollection productCollection = new ProductCollection();

        public void addMoney(double money)
        {
            if (introducedMoney.ContainsKey(money))
            {
                introducedMoney[money] += 1;
                totalMoney += money;
            }
            else
            {
                throw new Exception("Money not accepted");
            }
        }

        public bool IsEnoughMoney(int productId) {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            return totalMoney >= productPrice;
        }

        public bool BuyProductCash(int productId)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            IPayment payment = new CashPayment(introducedMoney,totalMoney);
            
            if (productPrice<=totalMoney)
            {
                productCollection.DecreaseProductQuantity(productId);
                payment.Pay(productPrice, totalMoney);
                return true;
            }
            return false;
        }
        

        public void AddProductToList(string name, int quantity, double price)
        {
            this.productCollection.AddProduct(name, quantity, price); 
        }

        public void UpdateProductInList(int productId, string name, int quantity, double price)
        {
            this.productCollection.UpdateProduct(productId, name, quantity, price);   
        }

        public void DeleteProductFromList(int productId)
        {
            this.productCollection.RemoveProduct(productId);   
        }

        
        public List<Product> GetProductsList()
        {
            return productCollection.GetProducts();
        }

    }
}
