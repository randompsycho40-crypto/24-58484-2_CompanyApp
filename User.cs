using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeeDetails
{
    class User
    {
        private static string myConn =
            ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public int ValidateLogin(string username, string password)
        {
            const string query =
                "SELECT UserID " +
                "FROM dbo.Users " +
                "WHERE Username = @Username AND Password = @Password";

            using (SqlConnection con = new SqlConnection(myConn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        return Convert.ToInt32(result);

                    return 0;
                }
            }
        }

        public bool UsernameExists(string username)
        {
            const string query =
                "SELECT COUNT(*) " +
                "FROM dbo.Users " +
                "WHERE Username = @Username";

            using (SqlConnection con = new SqlConnection(myConn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    con.Open();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public bool RegisterUser(string username, string password)
        {
            const string query =
                "INSERT INTO dbo.Users (Username, Password) " +
                "VALUES (@Username, @Password)";

            using (SqlConnection con = new SqlConnection(myConn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
        }
    }
}