using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Removed { get; set; }
        public DateTime? RemovalDate { get; set; }
        public List<Product> products { get; set; }

        public static CategoryDTO Create(Category category)
        {
            var dto = new CategoryDTO();

            dto.Id = category.Id;
            dto.Name = category.Name;
            dto.Description = category.Description;
            dto.Removed = category.Removed;
            dto.RemovalDate = category.RemovalDate;
            dto.products = category.products;
            return dto;
        }

        public static List<CategoryDTO> CreateList(List<Category> categoryList)
        {
            var dtoList = new List<CategoryDTO>();

            foreach (var c in categoryList)
            {
                dtoList.Add(Create(c));
            }

            return dtoList;
        }
    }  
}
