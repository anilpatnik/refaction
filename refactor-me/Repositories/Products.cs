using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace refactor_me.Repositories
{
    public class Products : IProducts
    {
        private readonly ProductsContext _context;

        public Products(ProductsContext context)
        {
            _context = context;
        }

        //save changes to database
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        //gets all products
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList<Product>();
        }

        //finds all products matching the specified name
        public IEnumerable<Product> Find(string name)
        {
            return _context.Products.Where(e => e.Name.Contains(name)).ToList<Product>();
        }

        //gets the project that matches the specified ID - ID is a GUID
        public Product Find(Guid id)
        {
            return _context.Products.FirstOrDefault(e => e.Id == id);
        }

        //creates a new product
        public int Add(Product item)
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
            return SaveChanges();
        }

        //updates a product
        public int Update(Product item)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == item.Id);
            if (product != null)
            {
                product.Name = item.Name;
                product.Description = item.Description;
                product.Price = item.Price;
                product.DeliveryPrice = item.DeliveryPrice;
                return SaveChanges();
            }
            return 0;
        }

        //deletes a product and its options
        public int Delete(Guid id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return SaveChanges();
            }
            return 0;
        }
    }
}