using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models;

public class Item
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The order ID is required.")]
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    [Required(ErrorMessage = "The product ID is required.")]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Qty { get; set; }
}
