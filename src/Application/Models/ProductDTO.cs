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
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Removed { get; set; }
        public DateTime? RemovalDate { get; set; }


        public static ProductDTO Create(Product product)
        {
            //Completar...
            var dto = new ProductDTO();
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Price = product.Price;
            dto.Description = product.Description;
            dto.CreatedDate = product.CreatedDate;
            dto.LastUpdate = product.LastUpdate;
            dto.CategoryId = product.CategoryId;
            dto.Category = product.Category;
            dto.Removed = product.Removed;
            dto.RemovalDate = product.RemovalDate;

            return dto;
        }

        public static List<ProductDTO> CreateList(List<Product> productList)
        {
            //Completar...
            var dtoList = new List<ProductDTO>();

            foreach(var p in productList)
            {
                dtoList.Add(Create(p));
            }

            return dtoList;
        }
    }
}
