using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDTO>> GetAll(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var completeCategoryList = await _categoryRepository.GetAllAsync();
                return CategoryDTO.CreateList(completeCategoryList);
            }

            var filteredCategoryList = await _categoryRepository.GetByNameAsync(name.Trim());

            if (!filteredCategoryList.Any())
            {
                throw new NotFoundException($"No se encontró categoría con nombre: {name.Trim()}");
            }

            return CategoryDTO.CreateList(filteredCategoryList);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"No se encontró el recurso con id {id}");
            }

            return CategoryDTO.Create(category);
        }

        public async Task<CategoryDTO> Create(CreationCategoryDTO creationCategoryDto)
        {
            var newCategory = new Category();

            newCategory.Name = creationCategoryDto.Name;
            newCategory.Description = creationCategoryDto.Description;

            var categoryAdded = await _categoryRepository.CreateAsync(newCategory);

            return CategoryDTO.Create(categoryAdded);
        }

        public async Task Update(int id, CreationCategoryDTO creationCategoryDTO)
        {
            var categoryToUpdate = await _categoryRepository.GetByIdAsync(id);

            if (categoryToUpdate is null)
            {
                throw new NotFoundException($"No se encontró el recurso con id {id}");
            }

            categoryToUpdate.Name = creationCategoryDTO.Name;
            categoryToUpdate.Description = String.IsNullOrWhiteSpace(creationCategoryDTO.Description) ? null : creationCategoryDTO.Description;

            await _categoryRepository.UpdateAsync(categoryToUpdate);
        }

        public async Task Delete(int id)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(id);

            if (categoryToDelete is null)
            {
                throw new NotFoundException($"No se encontró el recurso con id {id}");
            }

            if (!categoryToDelete.Removed)
            {
                categoryToDelete.Removed = true;
                categoryToDelete.RemovalDate = DateTime.Now;

                foreach (var product in categoryToDelete.products)
                {
                    if (!product.Removed)
                    {
                        product.Removed = true;
                        product.RemovalDate = DateTime.Now;
                    }
                }

                await _categoryRepository.UpdateAsync(categoryToDelete);
            }
            //await _categoryRepository.DeleteAsync(categoryToDelete);
        }
    }
}