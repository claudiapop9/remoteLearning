using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;

namespace VendingMachine
{
    class ProductCollection
    {
        public void AddProduct(Product p)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                db.Products.Add(p);
                db.SaveChanges();
            }
        }

        public void UpdateProduct(Product p)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public void DecreaseProductQuantity(int productId)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                Product p = db.Products.Where(x => x.ProductsID == productId).FirstOrDefault();
                p.Quantity -= 1;
                UpdateProduct(p);
            }
        }
        public void RemoveProduct(int productId)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                Product p = db.Products.Where(x => x.ProductsID == productId).FirstOrDefault();
                db.Products.Remove(p);
                db.SaveChanges();
            }
        }

        public double GetProductPriceByKey(int id)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                Product p = db.Products.Where(x => x.ProductsID == id).FirstOrDefault();
                double price = (Double)p.Price;
                return price;
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (VMachineEntities db = new VMachineEntities())
            {
                products = db.Products.ToList<Product>();
            }
            return products;
        }

    }
}
