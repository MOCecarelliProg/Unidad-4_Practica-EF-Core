using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> Create(CreationProductDTO creationProductDto);
        Task<List<ProductDTO>> GetAll(int? categoryId, string? productName);
        Task<ProductDTO> GetById(int id);
        Task Update(int id, CreationProductDTO creationProductDto);
        Task Delete(int id);
        //Task<List<ProductDTO>> GetByCategory(int categoryId);
        //Task<List<ProductDTO>> GetByName(string productName);
    }
}
