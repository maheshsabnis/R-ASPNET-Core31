using Core_WebApp31.Models;
using Core_WebApp31.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core_WebApp31.Services
{
    public class CategoryRepository : IRepository<Category, int>
    {
        EAppSoppingContext ctx;
        public CategoryRepository(EAppSoppingContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Category> CreateAsync(Category entity)
        {
            var res = await ctx.Categories.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cat = await ctx.Categories.FindAsync(id);
            if (cat == null) return false;

            ctx.Categories.Remove(cat);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            var cat = await ctx.Categories.FindAsync(id);
            return cat;
        }

        public Task<Category> UpdateAsync(int id, Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
