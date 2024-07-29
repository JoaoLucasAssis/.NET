using MVC.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Client
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 80 characters.")]
    public string? Name { get; set; }

    [StringLength(11, ErrorMessage = "The phone number must be {1} characters long.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "The ZIP code is required.")]
    [StringLength(8, ErrorMessage = "The ZIP code must be 8 characters long.")]
    public string? CEP { get; set; }

    [Required(ErrorMessage = "The state is required.")]
    public BrazilianStates State { get; set; }

    [Required(ErrorMessage = "The city is required.")]
    [StringLength(60, ErrorMessage = "The city cannot exceed 60 characters.")]
    public string? City { get; set; }
}
