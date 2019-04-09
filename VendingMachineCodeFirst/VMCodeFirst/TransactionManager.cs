using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineCodeFirst
{
    class TransactionManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void AddTransaction(Transaction t)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    db.Transactions.Add(t);
                    db.SaveChanges();
                    log.Info("Transaction Added");
                }
            }
            catch (Exception)
            {
                log.Error("Db connection failed-transactions");
            }
        }

        public List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    transactions = db.Transactions.ToList<Transaction>();
                }

            }
            catch (Exception)
            {
                log.Error("Db connection");
            }
            return transactions;
        }
    }
}
