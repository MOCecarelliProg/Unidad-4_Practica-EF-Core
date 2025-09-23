using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetByCategoryAsync(int? categoryId, bool removed)
    {
        if (!removed)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }
        else
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId && p.Removed)
                .Include(p => p.Category)
                .ToListAsync();
        }    
    }

    public async Task<List<Product>> GetByNameAsync(string name, bool removed)
    {
        if (!removed)
        {
            return await _context.Products.Where(p => p.Name.Contains(name))
            .Include(p => p.Category)
            .ToListAsync();
        }
        else
        {
            return await _context.Products.Where(p => p.Name.Contains(name) && p.Removed)
            .Include(p => p.Category)
            .ToListAsync();
        }
    }

    public override async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<List<Product>> GetOnlyNotRemovedAsync()
    {
        return await _context.Products
            //.Where(p => p.Removed == false)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public override async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
