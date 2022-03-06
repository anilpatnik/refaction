using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.Repositories
{
    public interface IProducts
    {
        //gets all products
        Task<IEnumerable<Product>> GetAll();

        //finds all products matching the specified name
        Task<IEnumerable<Product>> Find(string name);

        //gets the project that matches the specified ID - ID is a GUID
        Task<Product> Find(Guid id);

        //creates a new product
        Task<int> Add(Product item);

        //updates a product
        Task<int> Update(Product item);

        //deletes a product and its options
        Task<int> Delete(Guid id);
    }
}