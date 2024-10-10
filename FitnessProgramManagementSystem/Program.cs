using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {


            FitnessProgramManager manager1 = new FitnessProgramManager();
            FitnessProgramRepository repository = new FitnessProgramRepository();
            bool exit = false;
            SetConnection();

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\nFitnessProgram Management System:");
                Console.WriteLine(" 1. Add a FitnessProgram");
                Console.WriteLine(" 2. View All FitnessPrograms");
                Console.WriteLine(" 3. Update a FitnessProgram");
                Console.WriteLine(" 4. Delete a FitnessProgram");
                Console.WriteLine(" 5. View a FitnessProgramByID");
                Console.WriteLine(" 6. Exit");
                Console.Write(" Choose an option:");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        CreateFitnessProgram(manager1, repository);
                        break;

                    case "2":
                        Console.Clear();
                        ReadFitnessPrograms(repository);
                        break;

                    case "3":
                        Console.Clear();
                        UpdateFitnessProgram(manager1, repository);
                        break;

                    case "4":
                        Console.Clear();
                        DeleteFitnessProgram(manager1, repository);
                        break;
                    case "5":
                        Console.Clear();
                        ReadFitnessProgramById(repository);
                        break;
                    case "6":
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Please select valid choice");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                }
            }

        }

        static void CreateFitnessProgram(FitnessProgramManager manager, FitnessProgramRepository repo)
        {
/*            Console.WriteLine("Enter FitnessProgram ID:");
            int id = int.Parse(Console.ReadLine());*/

            Console.WriteLine("Enter FitnessProgram Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter FitnessProgram Duration:");
            string duration = Console.ReadLine();

            /*Console.WriteLine("Enter FitnessProgram Price:");
            decimal price = decimal.Parse(Console.ReadLine());*/
            decimal price = manager.ValidateFitnessProgramPrice();

            //manager.CreateFitnessProgram(id, title, duration, price);
            repo.CreateFitnessProgram(title, duration, price);
        }

        static void UpdateFitnessProgram(FitnessProgramManager manager, FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID  to update:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter new Duration:");
            string duration = Console.ReadLine();

            /*Console.WriteLine("Enter new Price:");
            decimal price = decimal.Parse(Console.ReadLine());*/
            decimal price = manager.ValidateFitnessProgramPrice();


            //manager.UpdateFitnessProgram(id, title, duration, price);
            repo.UpdateFitnessProgram(id, title, duration, price);
        }

        static void DeleteFitnessProgram(FitnessProgramManager manager, FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID  to delete:");
            int id = int.Parse(Console.ReadLine());

           // manager.DeleteFitnessProgram(id);
            repo.DeleteFitnessProgram(id);
        }

        static void ReadFitnessProgramById( FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID  to View:");
            int id = int.Parse(Console.ReadLine());

            // manager.DeleteFitnessProgram(id);
            var program = repo.ReadFitnessProgramById(id);
            Console.WriteLine(program.ToString());
        }

        static void ReadFitnessPrograms(FitnessProgramRepository repo)
        {

            // manager.DeleteFitnessProgram(id);
            var programList = repo.ReadFitnessPrograms();
            foreach ( var program in programList)
            {
                Console.WriteLine(program.ToString());
            }
        }

        static void SetConnection()
        {
            string masterDbConnectionString = "Server=JENITTAH; Database=master; Trusted_Connection=True; TrustServerCertificate=True;";
            string fitnessDbConnectionString = "Server=JENITTAH; Database=FitnessProgramManagement; Trusted_Connection=True; TrustServerCertificate=True;";
            string dbQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='FitnessProgramManagement')
                    CREATE DATABASE FitnessProgramManagement;
                    ";

            string tableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FitnessPrograms' AND xtype='U')
                    CREATE TABLE FitnessPrograms(
                      FitnessProgramId INT IDENTITY(1,1) PRIMARY KEY,  
                      Title NVARCHAR(50) NOT NULL,
                      Duration NVARCHAR(50) NOT NULL,
                      Price DECIMAL(10,2) NOT NULL
                    );";
            string insertQuery = @"
                    INSERT INTO FitnessPrograms (Title, Duration, Price)
                    VALUES ('weight Training' ,'6 months', 10);";

            using (SqlConnection connection = new SqlConnection(masterDbConnectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(dbQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database created successfully");
                }
            }

            using(SqlConnection conn = new SqlConnection(fitnessDbConnectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(tableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table created successfully");
                }

                using(SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Sample data insert successfully");
                }

            }
            Console.ReadLine();


        }
    }
}
