using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Controller
    {

        private ProductCollection productCollection = new ProductCollection();

        public bool BuyProduct(int productId, double introducedMoney)
        {
            double productPrice = productCollection.GetProductPriceByKey(productId);
            Payment payment = new Payment(introducedMoney);
            if (payment.IsEnough(productPrice))
            {
                productCollection.DecreaseProductQuantity(productId);
                return true;
            }
            else { return false; }
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
