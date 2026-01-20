namespace ORM_Dapper;

public interface IProductRepository
{
    IEnumerable<Products> GetAllProducts();
    void CreateProduct(string name, double price, int categoryID);
    public void UpdateProductName( int productID, string newName);
    public void DeleteProduct( int productID );
}   
