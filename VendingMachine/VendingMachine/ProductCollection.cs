using System.Collections.Generic;

namespace VendingMachine
{
    class ProductCollection
    {
        private Dictionary<int, Product> products=new Dictionary<int, Product>();


        public void AddProduct(int id,Product product)
        {

            products.Add(id, product);
        }

        public void UpdateProduct(int productId, string name, int quantity, double price)
        {
            if (products.ContainsKey(productId))
            {
                products[productId].NameProperty = name;
                products[productId].QuantityProperty = quantity;
                products[productId].PriceProperty = price;
            }

        }
        public void DecreaseProductQuantity(int productId)
        {

            if (products.ContainsKey(productId))
            {
                products[productId].DecreaseQuantity();
            }

        }

        public void RemoveProduct(int productId)
        {
            products.Remove(productId);
        }

        public Product GetProductByKey(int id)
        {
            return products[id];
        }
        public Dictionary<int, Product> GetProducts()
        {
            return this.products;
        }
        public override string ToString()
        {
            string str="";
            foreach (int id in products.Keys) {
                str += id +" "+ products[id]+"\n";
            }
            return str;
        }


    }
}
