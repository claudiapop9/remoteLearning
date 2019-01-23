using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace VendingMachine
{
    class CashPayment : IPayment
    {
        private List<CashMoney> introducedMoney;
        double totalMoney=0;

        public CashPayment(List<CashMoney> introducedMoney)
        {
            this.introducedMoney = introducedMoney;
            foreach (CashMoney entry in introducedMoney)
            {
                double value = (double)entry.MoneyValue;
                int quantity = (Int32)entry.Quantity;
                this.totalMoney += value*quantity;
            }
            
        }
        public void Pay(double cost)
        {

            if (cost.Equals(totalMoney))
            {
                foreach (CashMoney entry in introducedMoney)
                {
                    double value = (double)entry.MoneyValue;
                    int quantity = (Int32)entry.Quantity;
                    UpdateMoney(value,quantity);
                }
            }
            else if (cost < totalMoney)
            {
                foreach (CashMoney entry in introducedMoney)
                {
                    double value = (double)entry.MoneyValue;
                    int quantity = (Int32)entry.Quantity;
                    UpdateMoney(value, quantity);
                }
                GiveChange(totalMoney - cost);
            }
        }
        public bool IsEnough(double cost)
        {
            return cost <= totalMoney;
        }

        public void UpdateMoney(double value, int quantity)
        {
            using (VMachineEntities db = new VMachineEntities())
            {
                CashMoney cashMoney = db.CashMoneys.Where(x => x.MoneyValue == value).FirstOrDefault();
                cashMoney.Quantity += quantity;
                db.Entry(cashMoney).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void GiveChange(double change)
        {
            List<CashMoney> money = new List<CashMoney>();
            List<CashMoney> changedMoney = new List<CashMoney>();

            using (VMachineEntities db = new VMachineEntities())
            {
                money = db.CashMoneys.ToList<CashMoney>();
                changedMoney = CalculateMinimum(money, change);
                for (int i = 0; i < changedMoney.Count; i++)
                {
                    CashMoney coinFromChange = changedMoney[i];
                    CashMoney cashMoney = db.CashMoneys.Where(x => x.MoneyValue == coinFromChange.MoneyValue).FirstOrDefault();
                    cashMoney.Quantity -= coinFromChange.Quantity;
                    db.Entry(cashMoney).State = EntityState.Modified;
                    db.SaveChanges();
                }   
            }
        }

        public static List<CashMoney> CalculateMinimum(List<CashMoney> coins, double change)
        {
            // used to store the minimum matches
            List<CashMoney> minimalMatch = null;
            int minimalCount = -1;

            List<CashMoney> subset = coins;
            for (int i = 0; i < coins.Count; i++)
            {
                List<CashMoney> matches = Calculate(subset, change);
                if (matches != null)
                {
                    int matchCount = matches.Sum(c =>(Int32)c.Quantity);
                    if (minimalMatch == null || matchCount < minimalCount)
                    {
                        minimalMatch = matches;
                        minimalCount = matchCount;
                    }
                }
                // reduce the list of possible coins
                subset = subset.Skip(1).ToList();
            }

            return minimalMatch;
        }
        public static List<CashMoney> Calculate(List<CashMoney> coins, double change, int start = 0)
        {
            for (int i = start; i < coins.Count; i++)
            {
                CashMoney coin = coins[i];

                if (coin.Quantity > 0 && coin.MoneyValue <= change)
                {
                    double moneyValue = (Double)coin.MoneyValue;
                    double remainder = change % moneyValue;
                    if (remainder < change)
                    {
                        double s = (change - remainder) / moneyValue;
                        int quantity = (Int32)coin.Quantity;
                        int howMany = (Int32)Math.Min(quantity, s);

                        List<CashMoney> matches = new List<CashMoney>();
                        matches.Add(new CashMoney(moneyValue, howMany));

                        double amount = howMany * moneyValue;
                        double changeLeft = change - amount;
                        if (changeLeft == 0)
                        {
                            return matches;
                        }

                        List<CashMoney> subCalc = Calculate(coins, changeLeft, i + 1);
                        if (subCalc != null)
                        {
                            matches.AddRange(subCalc);
                            return matches;
                        }
                    }
                }
            }
            return null;
        }        
    }
}
