using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace VendingMachine
{
    class CashPayment : IPayment
    {
        SqlConnection cs = new SqlConnection("Data Source=DESKTOP-DS0RQDM;Initial Catalog=VendingMachine;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        double cost;
        double totalMoney;
        private Dictionary<double, int> introducedMoney;

        public CashPayment(Dictionary<double, int> introducedMoney,double totalMoney)
        {
            this.introducedMoney = introducedMoney;
            this.totalMoney = totalMoney;
        }

        public void Pay(double cost, double money)
        {

            if (cost.Equals(money))
            {
                foreach (KeyValuePair<double, int> entry in introducedMoney)
                {
                    UpdateMoney(entry.Key, entry.Value);
                }
            }
            else if (cost < money)
            {
                foreach (KeyValuePair<double, int> entry in introducedMoney)
                {
                    UpdateMoney(entry.Key, entry.Value);
                }
                GiveChange(money-cost);
            }
        }

        public void UpdateMoney(double value, int quantity)
        {
            DataTable dataTable = ds.Tables[1];
            DataRow dr = dataTable.Select(("MoneyValue=\'" + (value.ToString() + "\'")))[1];
            quantity += Int32.Parse(dr[2].ToString());

            da.UpdateCommand = new SqlCommand("UPDATE CashMoney SET Quantity=@quantity where MoneyValue=@value", cs);
            da.UpdateCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
            da.UpdateCommand.Parameters.Add("@value", SqlDbType.Float).Value = value;

            cs.Open();
            da.UpdateCommand.ExecuteNonQuery();
            cs.Close();

        }

        public double GiveChange(double change)
        {
            throw new NotImplementedException();
        }

        public Dictionary<double, double> IntroducedMoney { get; set; }
        public double TotalMoney { get; set; }



    }
}
