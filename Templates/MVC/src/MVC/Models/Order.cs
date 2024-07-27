using MVC.ValueObjects;

namespace MVC.Models;

public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public OrderStatus Status { get; set; }
    public string? Observation {  get; set; }
    public ICollection<Item>? Items { get; set; }
}
