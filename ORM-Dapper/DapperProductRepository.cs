using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace ORM_Dapper;

public class DapperProductRepository : IProductRepository
{
    private readonly IDbConnection _connection;

    public DapperProductRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<Products> GetAllProducts()
    {
        return _connection.Query<Products>("SELECT * FROM products;");
    }
    
    public void CreateProduct( string name, double price, int categoryID )
    {
        _connection.Execute("INSERT INTO products (Name, Price, CategoryID ) VALUES (@name, @price, @categoryId);",
            new { name = name, price = price, categoryID = categoryID });
    }
    
    public void UpdateProductName( int productID, string newName )
    {
        _connection.Execute("UPDATE PRODUCTS SET Name = @newName WHERE Name = @newName;",
                new { newName = newName, productID = productID });
    }

    public void DeleteProduct(int productID)
    {
        _connection.Execute("DELETE from REVIEWS WHERE ProductID = @productID;",
            new { productID = productID });
        _connection.Execute("DELETE from SALES WHERE ProductID = @productID;",
            new { productID = productID });
        _connection.Execute("DELETE from PRODUCTS WHERE ProductID = @productID;",
            new { productID = productID });
    }
}