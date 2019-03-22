using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.IO;
using Newtonsoft.Json;

namespace VendingMachineCodeFirst
{
    class ProductCollection
    {
        private const string filePath = @".\currentDb.txt";
        private const string filePathAllStates = @".\all.txt";

        public void AddProduct(Product p)
        {
            using (var db = new VendMachineDbContext())
            {
                db.Products.Add(p);
                db.SaveChanges();
            }
            PersistData();
        }

        public void UpdateProduct(Product p)
        {
            using (var db = new VendMachineDbContext())
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            PersistData();
        }

        public void DecreaseProductQuantity(int productId)
        {
            using (var db = new VendMachineDbContext())
            {
                Product p = db.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                p.Quantity -= 1;
                UpdateProduct(p);
            }
        }
        public void RemoveProduct(int productId)
        {
            using (var db = new VendMachineDbContext())
            {
                Product p = db.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                db.Products.Remove(p);
                db.SaveChanges();
            }
            PersistData();
        }

        public double GetProductPriceByKey(int id)
        {
            using (var db = new VendMachineDbContext())
            {
                Product p = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
                double price = (Double)p.Price;
                return price;
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (var db = new VendMachineDbContext())
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
