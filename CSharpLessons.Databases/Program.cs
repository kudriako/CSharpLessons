using System;
using System.Data;
using System.Data.SqlClient;

namespace CSharpLessons.Databases
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CSharpLessons;Integrated Security=true";

            string selectSql = "SELECT Id, Name, ManagerId FROM Employees";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.Write("TABLE EMPLOYEE");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}, {reader[1]}, {reader[2]}");
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            Console.WriteLine();

            string employeeName = "Alice";
            string insertSql = $"INSERT INTO Employees (Id, Name, ManagerId) VALUES (1, '{employeeName}', NULL);";
            string employeeName2 = "Bob";
            string insertSql2 = $"INSERT INTO Employees (Id, Name, ManagerId) VALUES (2, '{employeeName2}', 1);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql + insertSql2, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine($"{result} operation(s) executed.");
                }
            }
            Console.WriteLine();

            string selectSqlWithJoin = "SELECT E.Id, E.Name, E.ManagerId, M.Name FROM Employees E LEFT JOIN Employees M ON E.ManagerId = M.Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectSqlWithJoin, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("TABLE EMPLOYEE");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}, {reader[1]}, {reader[2]}");
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            Console.WriteLine();

            string countSql = "SELECT COUNT(*) FROM Employees";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(countSql, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    Console.WriteLine($"COUNT: {result}");
                    connection.Close();
                }
            }
            Console.WriteLine();

            Console.WriteLine("DATASET");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT Id, Name, ManagerId FROM Employees";
                using(SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection))
                {
                    DataSet employees = new DataSet();
                    adapter.Fill(employees, "Employees");
                    DataTable dt = employees.Tables["Employees"];
                    foreach(DataRow row in dt.Rows)
                    {
                        Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["ManagerId"]}");
                    }
                }
            }
            Console.WriteLine();

            string deleteSql = "DELETE FROM Employees";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine($"{result} operation(s) executed.");
                }
            }
        }
    }
}
