using System;
using System.Data.SqlClient;

namespace TestUserLogin
{
    class Program
    {
        static string connectionString = "Persist Security Info=False;User ID=TestLogin;Password=123;Initial Catalog=Test;Server=PC\\MSSQLSERVER01";

        static void Main(string[] args)
        {
            Console.WriteLine("Test User Login!");

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Insert Data");
                Console.WriteLine("2. Fetch Data");
                Console.WriteLine("3. Update Data");
                Console.WriteLine("4. Delete Data");
                Console.WriteLine("5. Exit");

                int choice;
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        InsertData();
                        break;
                    case 2:
                        FetchData();
                        break;
                    case 3:
                        UpdateData();
                        break;
                    case 4:
                        DeleteData();
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void InsertData()
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconnection.Open();
                    Console.WriteLine("Connection opened successfully.");

                    Console.WriteLine("Enter your Name:");
                    String Name = Console.ReadLine();

                    Console.WriteLine("Enter your Age:");
                    int age = int.Parse(Console.ReadLine());

                    String InsertCommand = "INSERT INTO TestLogin(Name,Age) VALUES (@Name,@Age)";
                    using (SqlCommand cmd = new SqlCommand(InsertCommand, sqlconnection))
                    {
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Data inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        static void FetchData()
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconnection.Open();
                    Console.WriteLine("Connection opened successfully.");

                    Console.WriteLine("Fetching details from database:");
                    string SelectCommand = "SELECT Id, Name, Age FROM TestLogin";
                    using (SqlCommand select = new SqlCommand(SelectCommand, sqlconnection))
                    {
                        SqlDataReader reader = select.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string retrievedName = reader.GetString(1);
                            int retrievedAge = reader.GetInt32(2);

                            Console.WriteLine($"ID: {id}, Name: {retrievedName}, Age: {retrievedAge}");
                        }

                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No data found.");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        static void UpdateData()
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconnection.Open();
                    Console.WriteLine("Update Data");

                    Console.WriteLine("Enter the Id to Update");
                    int UpdateId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the new Name:");
                    String newName = Console.ReadLine();

                    Console.WriteLine("Enter the new Age:");
                    int newAge = int.Parse(Console.ReadLine());

                    string UpdateCommand = "UPDATE TestLogin SET  Name=@Name,Age=@Age WHERE Id=@Id";
                    using (SqlCommand update = new SqlCommand(UpdateCommand, sqlconnection))
                    {
                        update.Parameters.Add("@Name", newName);
                        update.Parameters.Add("@Age", newAge);
                        update.Parameters.Add("@Id", UpdateId);
                        int rows = update.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            Console.WriteLine("Updated SuccessFully");
                        }
                        else
                        {
                            Console.WriteLine("Failed To Update");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void DeleteData()
        {
            using(SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconnection.Open();
                    Console.WriteLine("Success");

                    Console.WriteLine("Enter the Id to Delete");
                    int DeleteId=int.Parse(Console.ReadLine());

                    String DeleteCommand = "DELETE FROM TestLogin WHERE Id=@Id";
                    using(SqlCommand delete = new SqlCommand(DeleteCommand, sqlconnection))
                    {
                        delete.Parameters.AddWithValue("@Id",DeleteId);
                        int delrows = delete.ExecuteNonQuery();
                        if(delrows > 0)
                        {
                            Console.WriteLine("Delete Success");
                        }
                        else
                        {
                            Console.WriteLine("Failed");
                        }
                    }

                }catch (Exception ex) { Console.WriteLine(ex); }
            }
        }
    }
}
