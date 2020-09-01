using Core_WebApp31.Models;
using Core_WebApp31.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp31.Services
{
    public class ProductRepository : IRepository<Product, int>
    {
        public Task<Product> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(int id, Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
