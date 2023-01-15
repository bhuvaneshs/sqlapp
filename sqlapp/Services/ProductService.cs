using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "gobuildthings.database.windows.net";
        private static string db_database = "appdb";
        private static string db_user = "dbadmin";
        private static string db_password = "Blazing2023$";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.InitialCatalog = db_database;
            _builder.Password = db_password;
            _builder.UserID= db_user;

            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection connection= GetConnection();
            List<Product> products = new();
            string statement = "Select ProductID,ProductName,Quantity from Products";

            connection.Open();

            SqlCommand cmd = new(statement, connection);
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }
            connection.Close();
            return products;
        }

    }
}
