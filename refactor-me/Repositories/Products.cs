using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace refactor_me.Repositories
{
    public class Products : IProducts
    {
        private ProductsContext _context;

        public Products(ProductsContext context)
        {
            _context = context;
        }

        //save changes to database
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        //gets all products
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync<Product>();
        }

        //finds all products matching the specified name
        public async Task<IEnumerable<Product>> Find(string name)
        {
            return await _context.Products.Where(e => e.Name.Contains(name)).ToListAsync<Product>();
        }

        //gets the project that matches the specified ID - ID is a GUID
        public async Task<Product> Find(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(e => e.Id == id);
        }

        //creates a new product
        public async Task<int> Add(Product item)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                DeliveryPrice = item.DeliveryPrice
            };
            _context.Products.Add(product);
            return await SaveChanges();
        }

        //updates a product
        public async Task<int> Update(Product item)
        {
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == item.Id);
            if (product != null)
            {
                product.Name = item.Name;
                product.Description = item.Description;
                product.Price = item.Price;
                product.DeliveryPrice = item.DeliveryPrice;
                return await SaveChanges();
            }
            return 0;
        }

        //deletes a product and its options
        public async Task<int> Delete(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return await SaveChanges();
            }
            return 0;
        }
    }
}