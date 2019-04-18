using System;
using Newtonsoft.Json;

namespace VendingMachineAdmin
{
    class Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string AddProductToList(string tupleItem1, int tupleItem2, double tupleItem3)
        {
            try
            {
                Product product=new Product(tupleItem1,tupleItem2,tupleItem3);
                return AdminSocket.StartListening("ADD" + JsonConvert.SerializeObject(product));
                
            }
            catch (Exception ex)
            {
                log.Error("Add Product");
            }

            return "ERROR";
        }

        public string UpdateProductInList(int id, string tupleItem1, int tupleItem2, double tupleItem3)
        {
            try
            {
                Product product = new Product(id, tupleItem1, tupleItem2, tupleItem3);
                return AdminSocket.StartListening("UPDATE" + JsonConvert.SerializeObject(product));

            }
            catch (Exception)
            {
                log.Error("Update Product");
            }

            return "ERROR";
        }

        public string DeleteProductFromList(int id2)
        {
            return AdminSocket.StartListening("DELETE" + JsonConvert.SerializeObject(id2));
        }

        public string Refill()
        {
            return AdminSocket.StartListening("REFILL");
        }

        public string GenerateReport()
        {
            throw new System.NotImplementedException();
        }
    }
}
