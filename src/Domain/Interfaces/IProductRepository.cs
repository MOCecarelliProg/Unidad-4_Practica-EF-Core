using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Product Create(Product newProduct);

        //List<Product> GetAll();
        //Product GetById(int id);
        //void Update(Product updatedProduct);
        //void Delete(int id);
    }
}
