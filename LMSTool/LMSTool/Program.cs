using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LMSTool
{
    internal class Program
    {
        public static DataTable dt;
        static void Main(string[] args)
        {
            Program program = new Program();
            GetTrainingData();
        }
        public static void GetTrainingData()
        {
            // Connection string to your database
            string connectionString = "Data Source=PRE08920436035B;Initial Catalog=LMS;Integrated Security=True;";

            // SQL query to fetch data
            string query = "select EmployeeId,Skill,Rating  from PerformanceFeedback$ where rating in ('3', '4')";

            // Create a DataTable to hold the data


            try
            {
                // Establish connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Use SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                string rating = string.Empty;
                string empid = string.Empty;
                string skill = string.Empty;
                string courseid = string.Empty;
                string coursename = string.Empty;
                string leve = string.Empty;
                string duration = string.Empty;

                // Display data (optional)
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        rating += item.ToString();
                        empid += item.ToString();
                        skill += item.ToString();

                        if (rating == "3" || rating == "4")
                        {
                            string query1 = "select CourseId,CourseName,Level,Duration  from CourseCatalog$ where skill='" + skill + "'";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open the connection
                                connection.Open();

                                // Create a SqlCommand
                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    // Use SqlDataAdapter to fill the DataTable
                                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                    {
                                        adapter.Fill(dt);
                                    }
                                }
                            }
                            foreach (DataRow drow in dt.Rows)
                            {
                                foreach (var dtitem in row.ItemArray)
                                {
                                    courseid += dtitem.ToString();
                                    coursename += dtitem.ToString();
                                    leve += dtitem.ToString();
                                    string Insertquery = "INSERT INTO trainingAssigned (Name, Role, Column3) VALUES (@Value1, @Value2, @Value3)";

                                    // Values to insert
                                    string value1 = "SampleData1";
                                    int value2 = 123;
                                    DateTime value3 = DateTime.Now;

                                    try
                                    {
                                        // Establish connection
                                        using (SqlConnection connection = new SqlConnection(connectionString))
                                        {
                                            // Open connection
                                            connection.Open();

                                            // Create SQL command
                                            using (SqlCommand command = new SqlCommand(query, connection))
                                            {
                                                // Add parameters to prevent SQL injection
                                                command.Parameters.AddWithValue("@Value1", value1);
                                                command.Parameters.AddWithValue("@Value2", value2);
                                                command.Parameters.AddWithValue("@Value3", value3);

                                                // Execute the query
                                                int rowsAffected = command.ExecuteNonQuery();
                                                Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // Handle exceptions
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }
                                    duration += dtitem.ToString();

                                }
                            }
                        }

                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
