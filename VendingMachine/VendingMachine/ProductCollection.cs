using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.IO;
using Newtonsoft.Json;

namespace VendingMachine
{
    class ProductCollection
    {
        private string filePath = @"E:\computer\Documents\iQuest\remotelearning\VendingMachine\currentDb.txt";
        private string filePathAllStates = @"E:\computer\Documents\iQuest\remotelearning\VendingMachine\all.txt";

        public void AddProduct(Product p)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                db.Products.Add(p);
                db.SaveChanges();
            }
            PersistData();
        }

        public void UpdateProduct(Product p)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            PersistData();
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
            PersistData();
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
        public void PersistData()
        {
            WriteCurrentState(filePath);
            AppendCurrentState(filePathAllStates);
        }
        public void WriteCurrentState(string filePath)
        {

            using (StreamWriter myFile = new StreamWriter(filePath))
            {
                List<Product> products = GetProducts();
                foreach (Product p in products)
                {
                    myFile.WriteLine(JsonConvert.SerializeObject(p));
                }

            }
        }
        public void AppendCurrentState(string filePath)
        {
            List<Product> products = GetProducts();
            foreach (Product p in products)
            {
                File.AppendAllLines(filePath, new[] { JsonConvert.SerializeObject(p) });

            }

        }

    }
}
