using MVC.ValueObjects;
using System.ComponentModel.DataAnnotations;

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

    [Required(ErrorMessage = "The availability of the product must be specified.")]
    public bool IsAvailable { get; set; }
}
