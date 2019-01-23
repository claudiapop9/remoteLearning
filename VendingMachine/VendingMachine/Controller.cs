using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Controller
    {
        private List<CashMoney> introducedMoney= new List<CashMoney>();
        private ProductCollection productCollection = new ProductCollection();
        private IPayment payment;

        ///-----Product------------
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

        public bool IsEnoughCashMoney(int productId) {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            double totalMoney=0;
            foreach (CashMoney entry in introducedMoney)
            {
                double value = (double)entry.MoneyValue;
                int quantity = (Int32)entry.Quantity;
                totalMoney += value * quantity;
            }
            return productPrice <= totalMoney;
        }
        public void addMoney(double money)
        {
            CashMoney cashMoney = new CashMoney(money, 1);
            introducedMoney.Add(cashMoney);
        }

        public bool BuyProductCash(int productId)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            payment = new CashPayment(introducedMoney);

            if (payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(productId);
                payment.Pay(productPrice);
                return true;
            }
            return false;
        }
        public bool BuyProductByCard(int productId, string cardNo, string pin)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            payment = new CardPayment(cardNo,pin);

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
