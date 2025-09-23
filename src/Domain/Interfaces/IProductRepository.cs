using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<List<Product>> GetByCategoryAsync(int? categoryId, bool removed);
        Task<List<Product>> GetByNameAsync(string name, bool removed);
        Task<List<Product>> GetOnlyNotRemovedAsync();
    }
}
