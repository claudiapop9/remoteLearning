using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VendingMachine
{
    class ProductCollection
    {

        SqlConnection cs = new SqlConnection("Data Source=DESKTOP-DS0RQDM;Initial Catalog=VendingMachine;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public ProductCollection()
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Products", cs);
            ds.Clear();
            da.Fill(ds);
        }

        public void AddProduct(string name, int quantity, double price)
        {
            da.InsertCommand = new SqlCommand("INSERT INTO Products VALUES (@name, @quantity, @price)", cs);
            da.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            da.InsertCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
            da.InsertCommand.Parameters.Add("@price", SqlDbType.Float).Value = price;

            cs.Open();
            da.InsertCommand.ExecuteNonQuery();
            cs.Close();
        }

        public void UpdateProduct(int productId, string name, int quantity, double price)
        {

            da.UpdateCommand = new SqlCommand("UPDATE Products SET ProductName=@name, Quantity=@quantity, Price=@price where ProductsId=@id", cs);
            da.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            da.UpdateCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
            da.UpdateCommand.Parameters.Add("@price", SqlDbType.Float).Value = price;
            da.UpdateCommand.Parameters.Add("@id", SqlDbType.Int).Value = productId;

            cs.Open();
            da.UpdateCommand.ExecuteNonQuery();
            cs.Close();

        }

        public void DecreaseProductQuantity(int productId)
        {
            DataTable dataTable = ds.Tables[0];
            DataRow dr = dataTable.Select(("ProductsId=\'" + (productId.ToString() + "\'")))[0];
            int rowIndex = dataTable.Rows.IndexOf(dr);

            string name = ds.Tables[0].Rows[rowIndex][1].ToString();
            int quantity = -1 + Int32.Parse(ds.Tables[0].Rows[rowIndex][2].ToString());
            double price = Double.Parse(ds.Tables[0].Rows[rowIndex][2].ToString());
            UpdateProduct(productId, name, quantity, price);

        }

        public void RemoveProduct(int productId)
        {
            da.DeleteCommand = new SqlCommand("DELETE FROM Products WHERE ProductsID=@id", cs);
            da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value = productId;

            cs.Open();
            da.DeleteCommand.ExecuteNonQuery();
            cs.Close();

            ds.Clear();
            da.Fill(ds);
        }

        public double GetProductPriceByKey(int id)
        {
            DataTable dataTable = ds.Tables[0];
            DataRow dr = dataTable.Select(("ProductsId=\'" + (id.ToString() + "\'")))[0];
            int rowIndex = dataTable.Rows.IndexOf(dr);
            double price = Double.Parse(ds.Tables[0].Rows[rowIndex][3].ToString());
            return price;
        }

        public List<Product> GetProducts()
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Products", cs);
            ds.Clear();
            da.Fill(ds);
            List<Product> products = new List<Product>();
            DataTableReader reader = ds.Tables[0].CreateDataReader();
            if (reader.HasRows)
            {
                int productId, quantity;
                string name;
                double price;
                while (reader.Read())
                {
                    int i = 0;
                    while (i < reader.FieldCount)
                    {
                        productId = Int32.Parse(reader[i].ToString());
                        name = reader[i + 1].ToString();
                        quantity = Int32.Parse(reader[i + 2].ToString());
                        price = Double.Parse(reader[i + 3].ToString());
                        Product product = new Product(productId, name, quantity, price);
                        products.Add(product);
                        i += 4;
                    }

                }
            }
            return products;
        }

    }
}
