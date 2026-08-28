using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetails
{
    class Employee
    {
        private static string myConn =
            ConfigurationManager
            .ConnectionStrings["connString"]
            .ConnectionString;

        public string EmpId { get; set; }

        public string EmpName { get; set; }

        public string Age { get; set; }

        public string ContactNo { get; set; }

        public string Gender { get; set; }

        // UserID of the user who created the employee
        public int CreatedBy { get; set; }


        // Show the creator's Username instead of only the UserID
        private const string SelectQuery =
            "SELECT " +
            "e.EmpId, " +
            "e.EmpName, " +
            "e.EmpAge, " +
            "e.EmpContact, " +
            "e.EmpGender, " +
            "u.Username AS CreatedBy " +
            "FROM dbo.Emp_details e " +
            "LEFT JOIN dbo.Users u ON e.CreatedBy = u.UserID " +
            "ORDER BY e.EmpId";


        private const string InsertQuery =
            "INSERT INTO dbo.Emp_details " +
            "(EmpId, EmpName, EmpAge, EmpContact, EmpGender, CreatedBy) " +
            "VALUES " +
            "(@EmpId, @EmpName, @EmpAge, @EmpContact, @EmpGender, @CreatedBy)";


        // CreatedBy is NOT changed during update.
        // It represents the original creator of the employee record.
        private const string UpdateQuery =
            "UPDATE dbo.Emp_details " +
            "SET EmpName = @EmpName, " +
            "EmpAge = @EmpAge, " +
            "EmpContact = @EmpContact, " +
            "EmpGender = @EmpGender " +
            "WHERE EmpId = @EmpId";


        private const string DeleteQuery =
            "DELETE FROM dbo.Emp_details " +
            "WHERE EmpId = @EmpId";


        public DataTable GetEmployees()
        {
            DataTable datatable = new DataTable();

            using (SqlConnection con =
                   new SqlConnection(myConn))
            {
                con.Open();

                using (SqlCommand com =
                       new SqlCommand(SelectQuery, con))
                {
                    using (SqlDataAdapter adapter =
                           new SqlDataAdapter(com))
                    {
                        adapter.Fill(datatable);
                    }
                }
            }

            return datatable;
        }


        public bool InsertEmployee(Employee employee)
        {
            int rows;

            using (SqlConnection con =
                   new SqlConnection(myConn))
            {
                con.Open();

                using (SqlCommand com =
                       new SqlCommand(InsertQuery, con))
                {
                    com.Parameters.AddWithValue(
                        "@EmpId",
                        employee.EmpId);

                    com.Parameters.AddWithValue(
                        "@EmpName",
                        employee.EmpName);

                    com.Parameters.AddWithValue(
                        "@EmpAge",
                        employee.Age);

                    com.Parameters.AddWithValue(
                        "@EmpContact",
                        employee.ContactNo);

                    com.Parameters.AddWithValue(
                        "@EmpGender",
                        employee.Gender);

                    // Store logged-in user's UserID
                    com.Parameters.AddWithValue(
                        "@CreatedBy",
                        employee.CreatedBy);

                    rows = com.ExecuteNonQuery();
                }
            }

            return rows > 0;
        }


        public bool UpdateEmployee(Employee employee)
        {
            int rows;

            using (SqlConnection con =
                   new SqlConnection(myConn))
            {
                con.Open();

                using (SqlCommand com =
                       new SqlCommand(UpdateQuery, con))
                {
                    com.Parameters.AddWithValue(
                        "@EmpName",
                        employee.EmpName);

                    com.Parameters.AddWithValue(
                        "@EmpAge",
                        employee.Age);

                    com.Parameters.AddWithValue(
                        "@EmpContact",
                        employee.ContactNo);

                    com.Parameters.AddWithValue(
                        "@EmpGender",
                        employee.Gender);

                    com.Parameters.AddWithValue(
                        "@EmpId",
                        employee.EmpId);

                    rows = com.ExecuteNonQuery();
                }
            }

            return rows > 0;
        }


        public bool DeleteEmployee(Employee employee)
        {
            int rows;

            using (SqlConnection con =
                   new SqlConnection(myConn))
            {
                con.Open();

                using (SqlCommand com =
                       new SqlCommand(DeleteQuery, con))
                {
                    com.Parameters.AddWithValue(
                        "@EmpId",
                        employee.EmpId);

                    rows = com.ExecuteNonQuery();
                }
            }

            return rows > 0;
        }
    }
}
