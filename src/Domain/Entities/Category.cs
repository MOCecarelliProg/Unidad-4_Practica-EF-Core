using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Removed { get; set; } = false;
        public DateTime? RemovalDate { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
    }
}
