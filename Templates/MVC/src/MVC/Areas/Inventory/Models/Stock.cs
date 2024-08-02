using MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC.Areas.Inventory.Models;

public class Stock
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Qty { get; set; }

    [Required(ErrorMessage = "The store ID is required.")]
    public int StoreId { get; set; }
    public Store? Store { get; set; }

    public ICollection<Product>? Products { get; set; }
}

