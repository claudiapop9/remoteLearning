using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;


namespace VendingMachineCodeFirst
{
    class ProductCollection
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       

        public void AddProduct(Product p)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                }
            }
            catch (Exception) {
                log.Error("Db connection failed-ADD");
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
                
            }
            catch (Exception)
            {
                log.Error("Db connection failed-UPDATE");
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
                log.Error("Db connection failed-DecreaseProd");
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
                log.Error("Db connection failed-GET product by KEY");
            }
            return -1;
        }
       
        public bool Refill() {

            try
            {
                using (var db = new VendMachineDbContext())
                {
                    List<Product> productQuantity = GetProductsToRefill();

                    foreach (Product prod in productQuantity) {
                        prod.Quantity = 10;
                        db.Entry(prod).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                log.Info("REFILL successful");
                
                return true;
            }
            catch (Exception)
            {
                log.Error("REFILL failed");
                return false;
            }

        }

        public List<Product> GetProductsToRefill()
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    List<Product> productQuantity = (from product in db.Products
                        where (product.Quantity != 10)
                        select product).ToList();
                    return productQuantity;
                }

            }
            catch (Exception)
            {
                log.Error("FIND refill products failed");
            }

            return new List<Product>();
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
                log.Error("Db connection-GET Prod");
            }
            return products;
        }

       

    }
}
