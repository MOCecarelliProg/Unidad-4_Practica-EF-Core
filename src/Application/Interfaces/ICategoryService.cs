using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetById(int id);
        Task<List<CategoryDTO>> GetAll(string? name);
        Task<CategoryDTO> Create(CreationCategoryDTO creationCategoryDTO);
        Task Update(int id, CreationCategoryDTO creationCategoryDTO);
        Task Delete(int id);
        //Task<List<CategoryDTO>> GetByName(string name);
    }
}
