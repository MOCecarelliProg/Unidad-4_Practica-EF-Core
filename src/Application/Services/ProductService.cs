using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductDTO> Create(CreationProductDTO creationProductDto)
    {
        var category = await _categoryRepository.GetByIdAsync(creationProductDto.CategoryId);

        if (category is null)
        {
            throw new ValidationException("El id de la categoría es inválido");
        }

        if (creationProductDto.Price <= 0)
        {
            throw new ValidationException("El precio del producto debe ser mayor a cero");
        }

        var newProduct = new Product();

        newProduct.Name = creationProductDto.Name;
        newProduct.Price = creationProductDto.Price;
        newProduct.Description = String.IsNullOrWhiteSpace(creationProductDto.Description) ? null : creationProductDto.Description;
        newProduct.CategoryId = creationProductDto.CategoryId;
        newProduct.Category = category;

        var productAdded = await _productRepository.CreateAsync(newProduct);

        return ProductDTO.Create(productAdded);
    }

    public async Task Delete(int id)
    {
        var productToRemove = await _productRepository.GetByIdAsync(id);

        if (productToRemove is null)
        {
            throw new NotFoundException($"No se encontró el recurso con id {id}");
        }

        if (!productToRemove.Removed)
        {
            productToRemove.Removed = true;
            productToRemove.RemovalDate = DateTime.Now;

            await _productRepository.UpdateAsync(productToRemove);
        }
    }

    public async Task<List<ProductDTO>> GetAll(int? categoryId, string? productName)
    {
        bool catIdFlag = (categoryId is null || categoryId < 1);
        bool prodNameFlag = String.IsNullOrWhiteSpace(productName);

        if (catIdFlag && prodNameFlag)
        {
            var productList = await _productRepository.GetOnlyNotRemovedAsync();
            return ProductDTO.CreateList(productList);

        }
        else if (catIdFlag && !prodNameFlag)
        {
            var productList = await _productRepository.GetByNameAsync(productName.Trim(), false);

            if (!productList.Any())
            {
                throw new NotFoundException($"No se encontró ningún producto que contenga: {productName.Trim()}");
            }

            return ProductDTO.CreateList(productList);

        }
        else if (!catIdFlag && prodNameFlag)
        {
            var productList = await _productRepository.GetByCategoryAsync(categoryId, false);

            if (!productList.Any())
            {
                throw new NotFoundException($"No se encontró ningún producto en la categoría {categoryId}");
            }

            return ProductDTO.CreateList(productList);
        }
        else
        {
            var productList = await _productRepository.GetByCategoryAsync(categoryId, false);

            if (productList.Any())
            {
                var filteredList = productList.Where(p => p.Name.Contains(productName.Trim(), StringComparison.OrdinalIgnoreCase) && !p.Removed).ToList();

                if (filteredList.Any())
                {
                    return ProductDTO.CreateList(filteredList);
                }
                else
                {
                    throw new NotFoundException($"No se encontró ningún producto que contenga: {productName}");
                }
            }
            else
            {
                throw new NotFoundException($"No se encontró ningún producto en la categoría {categoryId}");
            }
        }
    }
    

    public async Task<ProductDTO> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new NotFoundException($"No se encontró el recurso con id {id}");
        }

        return ProductDTO.Create(product);
    }

    public async Task Update(int id, CreationProductDTO creationProductDto)
    {
        var productToUpdate = await _productRepository.GetByIdAsync(id);

        if (productToUpdate is null)
        {
            throw new NotFoundException($"No se encontró el recurso con id {id}");
        }

        if (productToUpdate.CategoryId != creationProductDto.CategoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new ValidationException("El id de la categoría es inválido");
            }
        }

        if (creationProductDto.Price <= 0)
        {
            throw new ValidationException("El precio del producto debe ser mayor a cero");
        }

        productToUpdate.Name = creationProductDto.Name;
        productToUpdate.Price = creationProductDto.Price;
        productToUpdate.Description = String.IsNullOrWhiteSpace(creationProductDto.Description) ? null : creationProductDto.Description;
        productToUpdate.LastUpdate = DateTime.Now;
        productToUpdate.CategoryId = creationProductDto.CategoryId;

        await _productRepository.UpdateAsync(productToUpdate);
    }

    /*public async Task<List<ProductDTO>> GetByCategory(int categoryId)
    {
        if (categoryId < 1)
        {
            throw new ValidationException("El id de la categoría no es válido");
        }

        List<Product> productList = await _productRepository.GetByCategoryAsync(categoryId);

        if (!productList.Any())
        {
            throw new NotFoundException("No existen productos con esa categoría");
        }

        return ProductDTO.CreateList(productList);
    }*/

    /*public async Task<List<ProductDTO>> GetByName(string productName)
    {
        if (String.IsNullOrWhiteSpace(productName))
        {
            throw new ValidationException("El nombre suministrado no es correcto");
        }

        List<Product> productList = await _productRepository.GetByNameAsync(productName.Trim());

        if (!productList.Any())
        {
            throw new NotFoundException("No existen productos con ese nombre");
        }

        return ProductDTO.CreateList(productList);
    }*/
}
