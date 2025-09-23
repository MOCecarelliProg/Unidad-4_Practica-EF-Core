using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetByNameAsync(string name)
        {
            return await _context.Categories.Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.products)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
