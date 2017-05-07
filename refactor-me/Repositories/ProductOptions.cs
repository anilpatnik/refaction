using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace refactor_me.Repositories
{
    public class ProductOptions : IProductOptions
    {
        private readonly ProductsContext _context;

        public ProductOptions(ProductsContext context)
        {
            _context = context;
        }

        //save changes to database
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        //finds all options for a specified product
        public IEnumerable<ProductOption> GetAll(Guid productId)
        {
            return _context.ProductOptions
                .Where(e => e.ProductId == productId)
                .ToList<ProductOption>();
        }

        //finds the specified product option for the specified product
        public ProductOption Find(Guid productId, Guid id)
        {
            return _context.ProductOptions
                .FirstOrDefault(e => e.ProductId == productId && e.Id == id);
        }

        //adds a new product option to the specified product
        public int Add(ProductOption item)
        {
            var productOption = new ProductOption
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                ProductId = item.ProductId
            };
            _context.ProductOptions.Add(productOption);
            return SaveChanges();
        }

        //updates the specified product option
        public int Update(ProductOption item)
        {
            var productOption = _context.ProductOptions.FirstOrDefault(e => e.Id == item.Id);
            if (productOption != null)
            {
                productOption.Name = item.Name;
                productOption.Description = item.Description;
                return SaveChanges();
            }
            return 0;
        }

        //deletes the specified product option
        public int Delete(Guid id)
        {
            var productOptions = _context.ProductOptions.FirstOrDefault(e => e.Id == id);
            if (productOptions != null)
            {
                _context.ProductOptions.Remove(productOptions);
                return SaveChanges();
            }
            return 0;
        }
    }
}