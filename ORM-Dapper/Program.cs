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
            
            // Add new department
            repo.AddDepartment("Large Appliances");
            
            departments = repo.GetDepartments();
           
            foreach (var department in departments)
            {
                // Should show new department above
                Console.WriteLine($"{department.DepartmentID} {department.Name}");
            }
            
            // Now let's create a new product
            var repo2 = new DapperProductRepository( conn );
            
            // Add jbl headphones to products table
            repo2.CreateProduct("JBL BT720 Headphones", 49.99, 4);

            var products = repo2.GetAllProducts();
            
            foreach (var product in products)
            {
                // Should show our new product jbl BT720 headphones
                Console.WriteLine($"{product.ProductID} {product.Name}  {product.Price}");
            }

            // Update the jbl headphones name
            repo2.UpdateProductName(950, "JBL NC770 Headphones");
            
            foreach (var product in products)
            {
                // Should show the jbl headphones new name
                Console.WriteLine($"{product.ProductID} {product.Name} {product.Price}");
            }
            
            // Delete the jbl headphones product
            repo2.DeleteProduct(950);
            
            products = repo2.GetAllProducts();
            
            foreach (var product in products)
            {
                // the jbl headphones we added should be gone 
                Console.WriteLine($"{product.ProductID} {product.Name} {product.Price}");
            }
        }
    }
}
