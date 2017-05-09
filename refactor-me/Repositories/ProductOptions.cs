using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace refactor_me.Repositories
{
    public class ProductOptions : IProductOptions
    {
        private ProductsContext _context;

        public ProductOptions(ProductsContext context)
        {
            _context = context;
        }

        //save changes to database
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        //finds all options for a specified product
        public async Task<IEnumerable<ProductOption>> GetAll(Guid productId)
        {
            return await _context.ProductOptions
                .Where(e => e.ProductId == productId)
                .ToListAsync<ProductOption>();
        }

        //finds the specified product option for the specified product
        public async Task<ProductOption> Find(Guid productId, Guid id)
        {
            return await _context.ProductOptions
                .FirstOrDefaultAsync(e => e.ProductId == productId && e.Id == id);
        }

        //adds a new product option to the specified product
        public async Task<int> Add(ProductOption item)
        {
            var productOption = new ProductOption
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                ProductId = item.ProductId
            };
            _context.ProductOptions.Add(productOption);
            return await SaveChanges();
        }

        //updates the specified product option
        public async Task<int> Update(ProductOption item)
        {
            var productOption = await _context.ProductOptions.FirstOrDefaultAsync(e => e.Id == item.Id);
            if (productOption != null)
            {
                productOption.Name = item.Name;
                productOption.Description = item.Description;
                return await SaveChanges();
            }
            return 0;
        }

        //deletes the specified product option
        public async Task<int> Delete(Guid id)
        {
            var productOptions = await _context.ProductOptions.FirstOrDefaultAsync(e => e.Id == id);
            if (productOptions != null)
            {
                _context.ProductOptions.Remove(productOptions);
                return await SaveChanges();
            }
            return 0;
        }
    }
}