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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string filePath = @".\currentDb.txt";
        private const string filePathAllStates = @".\all.txt";

        public void AddProduct(Product p)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                }
                PersistData();
            }
            catch (Exception) {
                log.Error("Db connection failed");
            }
        }

        public void UpdateProduct(Product p)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
                PersistData();
            }
            catch (Exception)
            {
                log.Error("Db connection failed");
            }
        }

        public void DecreaseProductQuantity(int productId)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    Product p = db.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                    p.Quantity -= 1;
                    UpdateProduct(p);
                }
            }
            catch (Exception)
            {
                log.Error("Db connection failed");
            }
        }
        public void RemoveProduct(int productId)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    Product p = db.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                    db.Products.Remove(p);
                    db.SaveChanges();
                }
                PersistData();
            }catch (Exception)
            {
                log.Error("Db connection failed-remove prod");
            }
        }

        public double GetProductPriceByKey(int id)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    Product p = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
                    double price = (Double)p.Price;
                    return price;
                }
            }
            catch (Exception)
            {
                log.Error("Db connection failed-get product by key");
            }
            return -1;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    products = db.Products.ToList<Product>();
                }
                
            }
            catch (Exception)
            {
                log.Error("Db connection");
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
            try
            {
                using (StreamWriter myFile = new StreamWriter(filePath))
                {
                    List<Product> products = GetProducts();
                    foreach (Product p in products)
                    {
                        myFile.WriteLine(JsonConvert.SerializeObject(p));
                    }
                    log.Info("Current state written in file");

                }
            }
            catch (Exception) {
                log.Error("File error");
            }
        }
        public void AppendCurrentState(string filePath)
        {
            
            List<Product> products = GetProducts();
            try
            {
                foreach (Product p in products)
                {
                    File.AppendAllLines(filePath, new[] { JsonConvert.SerializeObject(p) });
                }
                log.Info("Current state appended");
            }
            catch (ArgumentNullException)
            {
                log.Error("File path/contents is null");
            }
            catch (IOException)
            {
                log.Error("Error occurred while opening the file");
            }
            catch (Exception) {
                log.Error("File Error when appending");
            }

        }

    }
}
