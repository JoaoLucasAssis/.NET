using MVC.ValueObjects;

namespace MVC.Models;

public class Product
{
    public int Id { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ProductType ProductType { get; set; }
    public bool IsAvailable { get; set; }
}
