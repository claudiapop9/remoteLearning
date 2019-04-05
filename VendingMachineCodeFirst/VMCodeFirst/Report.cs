
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VendingMachineCodeFirst
{
    class Report
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void GenerateReport(string filePath, List<Product> list)
        {
            try
            {
                using (StreamWriter myFile = new StreamWriter(filePath))
                {
                    myFile.WriteLine("Report " + DateTime.Now);
                    myFile.WriteLine(GetMostBought(list));
                }
            }
            catch(Exception)
            {
                log.Error("Writing report");
            }
        }

        private string GetMostBought(List<Product> list)
        {
            string str = "Most Bought Products:\n";
            var result = list.GroupBy(p => p.Name)
                        .ToDictionary(x => x.Key,x => x.Sum(t => t.Quantity))
                        .OrderBy(x=>x.Value);
            
            foreach (var key in result)
            {
                str += key.Key.ToString();
                str += " \n";
            }

            return str;

        }
    }
}
