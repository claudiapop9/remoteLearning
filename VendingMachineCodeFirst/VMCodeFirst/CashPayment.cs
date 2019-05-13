﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace VendingMachineCodeFirst
{
//code review [Teo]: all methods are public, but some are only used within the class
//Consider not exposing(i.e. marking the methods as private) when they are not called from outside the class
//this also applies in CashPayment.cs, Data.cs
    class CashPayment : IPayment
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        List<double> acceptedDenominations = new List<double>() { 10, 5, 1, 0.5 };
        private List<CashMoney> introducedMoney = new List<CashMoney>();
        double totalMoney=0;
        
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
            AskForMoney(cost);
            return cost <= totalMoney;
        }

        public void AskForMoney(double cost) {
            try
            {
                Console.WriteLine("Introduce money:");
                double money = Double.Parse(Console.ReadLine());
                AddMoney(money);
                while (totalMoney < cost)
                {
                    Console.WriteLine($"Not enough.Introduce more: {cost - totalMoney}:");
                    money = Double.Parse(Console.ReadLine());
                    AddMoney(money);
                };
            }
            catch (Exception e) {
                log.Error("Wrong introduced money.");
                Console.WriteLine(e);
            }

        }

        public void AddMoney(double money)
        {
            if (acceptedDenominations.Contains(money))
            {
                CashMoney cashMoney = new CashMoney(money, 1);
                introducedMoney.Add(cashMoney);
                totalMoney += money;
            }
            else
            { throw new Exception("Money not accepted"); }
        }


        public void UpdateMoney(double value, int quantity)
        {
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    CashMoney cashMoney = db.Money.Where(x => x.MoneyValue == value).FirstOrDefault();
                    cashMoney.Quantity += quantity;
                    db.Entry(cashMoney).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception) {
                log.Error("Fail database connection\n");
            }
        }
        public void GiveChange(double change)
        {
            List<CashMoney> money = new List<CashMoney>();
            List<CashMoney> changedMoney = new List<CashMoney>();
            try
            {
                using (var db = new VendMachineDbContext())
                {
                    money = db.Money.ToList<CashMoney>();
                    changedMoney = CalculateMinimum(money, change);
                    Console.WriteLine("Change: ");
                    for (int i = 0; i < changedMoney.Count; i++)
                    {
                        CashMoney coinFromChange = changedMoney[i];
                        Console.Write(changedMoney[i] + " ");
                        CashMoney cashMoney = db.Money.Where(x => x.MoneyValue == coinFromChange.MoneyValue).FirstOrDefault();
                        cashMoney.Quantity -= coinFromChange.Quantity;
                        db.Entry(cashMoney).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    log.Info("GIVE Change success");
                }
            }
            catch (Exception) {
                log.Error("Fail database connection-GIVE Change");
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
