using MVC.Areas.Inventory.Models;
using MVC.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The product name is required.")]
    [StringLength(50, ErrorMessage = "The product name cannot exceed 50 characters.")]
    public string? Name { get; set; }

    [StringLength(60, ErrorMessage = "The product description cannot exceed 60 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The product price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The product type is required.")]
    public ProductType ProductType { get; set; }

    public bool IsAvailable => Qty > 0;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Qty => Stock?.Qty ?? 0;

    [Required(ErrorMessage = "The stock ID is required.")]
    public int StockId { get; set; }
    public Stock? Stock { get; set; }
}
