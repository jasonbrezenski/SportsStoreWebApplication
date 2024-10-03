using System.Data;
using Dapper;
using SportsStoreWebApplication.Models;

namespace SportsStoreWebApplication.Data;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _connection;

    public ProductRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public IEnumerable<Product> GetAllProducts()
    {
        return _connection.Query<Product>("SELECT * FROM products;");
    }

    public Product GetProduct(int id)
    {
        return _connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", new { id = id });
    }

    public void UpdateProduct(Product product)
    {
        _connection.Execute(
            "UPDATE products SET ProductName = @name, Category = @category, Price = @price, Stock = @stock WHERE ProductID = @id;",
            new { name = product.ProductName, category = product.Category, price = product.Price, stock = product.Stock, id = product.ProductID });
    }

    public void InsertProduct(Product productToInsert)
    { 
        _connection.Execute("INSERT INTO products (ProductName, Price) VALUES (@name, @price);",
                new {name = productToInsert.ProductName, price = productToInsert.Price });
    }

    public IEnumerable<Category> GetCategories()
    {
        return _connection.Query<Category>("SELECT * FROM categories;");
    }

    public Product AssignCategory()
    {
        var categoryList = GetCategories();
        var product = new Product();
        //product.Categories = categoryList;
        return product;
    }

    public void DeleteProduct(Product product)
    {
        _connection.Execute("DELETE FROM order_items WHERE ProductID = @id;", new { id = product.ProductID });
        _connection.Execute("DELETE FROM products WHERE ProductID = @id;", new { id = product.ProductID });
    }
}