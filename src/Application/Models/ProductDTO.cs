using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        public static ProductDTO Create(Product product)
        {
            //Completar...
            throw new NotImplementedException();
        }

        public static List<ProductDTO> CreateList(List<Product> productList)
        {
            //Completar...
            throw new NotImplementedException();
        }
    }
}
