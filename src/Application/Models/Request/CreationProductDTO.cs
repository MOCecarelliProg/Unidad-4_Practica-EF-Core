using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationProductDTO
    {
        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El precio es requerido.")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "El id de la categoría es requerido.")]
        public int CategoryId { get; set; }
    }
}
