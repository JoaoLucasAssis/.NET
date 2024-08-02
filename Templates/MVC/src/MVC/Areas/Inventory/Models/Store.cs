using System.ComponentModel.DataAnnotations;

namespace MVC.Areas.Inventory.Models;

public class Store
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The product name is required.")]
    [StringLength(50, ErrorMessage = "The product name cannot exceed 50 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The localtion is required.")]
    [StringLength(150, ErrorMessage = "The location cannot exceed 150 characters.")]
    public string? Location { get; set; }
    public ICollection<Stock>? Stocks { get; set; }
}

