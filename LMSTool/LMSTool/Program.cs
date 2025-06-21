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
            string rating = string.Empty;
            string empid = string.Empty;
            string skill = string.Empty;
            string courseid = string.Empty;
            string coursename = string.Empty;
            string level = string.Empty;
            string duration = string.Empty;
            string category = string.Empty;
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


                // Display data (optional)
                foreach (DataRow row in dt.Rows)
                {


                    rating = row["Rating"].ToString();
                    empid = row["EmployeeId"].ToString();
                    skill = row["Skill"].ToString();


                    if (rating == "3" || rating == "4")
                    {
                        string query1 = "select CourseId,CourseName,category,Level,Duration  from CourseCatalog$ where skill='" + skill + "'";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            // Open the connection
                            connection.Open();
                            //test
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

                            courseid = drow["CourseId"].ToString();
                            coursename = drow["CourseName"].ToString();
                            level = drow["Level"].ToString();
                            duration = drow["Duration"].ToString();
                            category = drow["Category"].ToString();
                            string Insertquery = "INSERT INTO trainingAssigned (Name,CourseId,CourseName,Category,Skill,Level,Duration) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9)";

                            // Values to insert
                            string value1 = empid;
                            string value2 = courseid;
                            string value3 = coursename;
                            string value4 = category;
                            string value5 = skill;
                            string value6 = level;
                            string value7 = duration;


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
                                        command.Parameters.AddWithValue("@Value4", value4);
                                        command.Parameters.AddWithValue("@Value5", value5);
                                        command.Parameters.AddWithValue("@Value6", value6);
                                        command.Parameters.AddWithValue("@Value7", value7);


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
