using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args) {
            
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            
            var repo = new DapperDepartmentRepository( conn );

            var departments = repo.GetDepartments();
            
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} {department.Name}");
            }

            repo.AddDepartment("Large Applicances");
            departments = repo.GetDepartments();
            
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} {department.Name}");
            }
        }
    }
}
