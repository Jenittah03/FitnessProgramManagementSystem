using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem
{
    internal class FitnessProgramRepository
    {
        private string fitnessDbConnectionString = "Server=JENITTAH; Database=FitnessProgramManagement; Trusted_Connection=True; TrustServerCertificate=True;";

        public void CreateFitnessProgram(string title, string duration, decimal price)
        {
            try
            {
                string insertQuery = @"INSERT INTO FitnessPrograms (Title, Duration, Price)
                                  VALUES(@title, @duration, @price);";
                using (SqlConnection conn = new SqlConnection(fitnessDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Program created successfully");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ ex.Message);
            }
        }

        public void UpdateFitnessProgram(int id, string title, string duration, decimal price)
        {
            try
            {
                string updateQuery = @"UPDATE FitnessPrograms SET Title=@title, Duration=@duration, Price=@price WHERE FitnessProgramId=@id";
                using (SqlConnection conn = new SqlConnection(fitnessDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Program updated successfully");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void DeleteFitnessProgram(int id)
        {
            try
            {
                string deleteQuery = @"DELETE FROM FitnessPrograms WHERE FitnessProgramId=@id";
                using (SqlConnection conn = new SqlConnection(fitnessDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Program deleted successfully");
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public FitnessProgram ReadFitnessProgramById(int id)
        {
            FitnessProgram program = null;
            try
            {  
                string getQuery = @"SELECT * FROM FitnessPrograms WHERE FitnessProgramId=@id";
                using (SqlConnection conn = new SqlConnection(fitnessDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        //cmd.ExecuteNonQuery();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                int programId = reader.GetInt32(0);
                                string title = reader.GetString(1);
                                string duration = reader.GetString(2);
                                decimal price = reader.GetDecimal(3);
                                program = new FitnessProgram(programId, title, duration, price);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return program;
        }

        public List<FitnessProgram> ReadFitnessPrograms()
        {
            var programsList = new List<FitnessProgram>();
            try
            {
                string getQuery = @"SELECT * FROM FitnessPrograms";
                using (SqlConnection conn = new SqlConnection(fitnessDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Program Lists: ");
                            while (reader.Read())
                            {
                                int programId = reader.GetInt32(0);
                                string title = reader.GetString(1);
                                string duration = reader.GetString(2);
                                decimal price = reader.GetDecimal(3);
                                programsList.Add(new FitnessProgram(programId, title, duration, price));  
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return programsList;
        }



    }
}
