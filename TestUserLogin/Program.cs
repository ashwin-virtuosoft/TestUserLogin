using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUserLogin
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            String connection = "Persist Security Info=False;User ID=TestLogin;Password=123;Initial Catalog=Test;Server=PC\\MSSQLSERVER01";
            using (SqlConnection sqlconnection =new  SqlConnection(connection))
            {
                try
                {
                    sqlconnection.Open();
                    Console.WriteLine("Succes");

                    for (int i = 0; i < 10; i++)
                    {

                        Console.WriteLine("Enter your Name");
                        String Name = Console.ReadLine();

                        Console.WriteLine("Enter your Age");
                        int age = int.Parse(Console.ReadLine());
                        Console.WriteLine("Check");

                        String InsertCommand = "INSERT INTO TestLogin(Name,Age) VALUES (@Name,@Age)";
                        using (SqlCommand cmd = new SqlCommand(InsertCommand, sqlconnection))
                        {
                            cmd.Parameters.Add("@Name", Name);
                            cmd.Parameters.Add("@Age", age);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Inserted");
                        }
                        Console.ReadKey();
                    }
                } catch (Exception ex) { 
                    Console.WriteLine(ex);
                }

            }



        }
        
           
        
    }

 }  

