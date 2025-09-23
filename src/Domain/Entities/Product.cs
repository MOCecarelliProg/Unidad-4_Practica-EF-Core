using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastUpdate { get; set; }
    public bool Removed { get; set; } = false;
    public DateTime? RemovalDate { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public Category Category { get; set; }
}
