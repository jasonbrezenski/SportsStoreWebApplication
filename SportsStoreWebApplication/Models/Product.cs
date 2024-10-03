namespace SportsStoreWebApplication.Models;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public int CategoryID { get; set; }
    
    public IEnumerable<Category> Categories { get; set; }
}