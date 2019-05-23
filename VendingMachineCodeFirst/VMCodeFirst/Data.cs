﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace VendingMachineCodeFirst
{
    public class Data
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string filePath;
        private readonly string filePathAll;

        public Data(string filePath, string filePathAll)
        {
            this.filePath = filePath;
            this.filePathAll = filePathAll;

        }
        public void PersistData(List<Product> products)
        {
            WriteCurrentState(products);
            AppendCurrentState(products);
        }

        private void WriteCurrentState(List<Product> products)
        {
            try
            {
                using (StreamWriter myFile = new StreamWriter(filePath))
          
                    foreach (Product p in products)
                    {
                        myFile.WriteLine(JsonConvert.SerializeObject(p));
                    }
                    log.Info("Current state written in file");

                
            }
            catch (Exception)
            {
                log.Error("File error");
            }
        }

        private void AppendCurrentState(List<Product> products)
        {
            try
            {
                foreach (Product p in products)
                {
                    File.AppendAllLines(filePathAll, new[] { JsonConvert.SerializeObject(p) });
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
            catch (Exception)
            {
                log.Error("File Error when appending");
            }

        }

        }
}
