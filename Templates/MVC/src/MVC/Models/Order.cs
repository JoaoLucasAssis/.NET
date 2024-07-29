using MVC.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models;

public class Order
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The client ID is required.")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    [Required(ErrorMessage = "The start date is required.")]
    public DateTime StartDate { get; set; }

    [NotMapped]
    public DateTime EndDate => StartDate.AddDays(30);

    [Required(ErrorMessage = "The order status is required.")]
    public OrderStatus Status { get; set; }

    [StringLength(512, ErrorMessage = "The observation cannot exceed 512 characters.")]
    public string? Observation { get; set; }

    public ICollection<Item>? Items { get; set; }
}
