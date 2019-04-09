
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VendingMachineCodeFirst
{
    class Report
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void GenerateReport(string filePath, List<Transaction> list)
        {
            try
            {
                using (StreamWriter myFile = new StreamWriter(filePath))
                {
                    myFile.WriteLine("Report " + DateTime.Now);
                    myFile.WriteLine(GetMostBought(list));
                    myFile.WriteLine(GetLastRefillDate(list));
                    log.Info("Report written successfully");
                }
            }
            catch (Exception)
            {
                log.Error("Writing report");
            }
        }

        private string GetMostBought(List<Transaction> list)
        {
            string str = "Most Bought Products:\n";
            var result = list.GroupBy(p => p.ProductId)
                .ToDictionary(x => x.Key, x => x.Count());

            foreach (var key in result)
            {
                str += key.Key.ToString();
                str += " \n";
            }

            return str;
        }

        private string GetLastRefillDate(List<Transaction> list)
        {
            string str = "Last Refill:\n";
            try
            {
                var result = list.Where(t => t.Type == "REFILL").Select(t => (t.Date)).OrderByDescending(t => t.Value);
                foreach (var key in result)
                {
                    str += key.ToString();
                    str += " \n";
                }
            }
            catch (Exception ex)
            {
                log.Error("GET REFILL failed");
            }

            return str;
        }
    }
}
