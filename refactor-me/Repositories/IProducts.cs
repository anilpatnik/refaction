using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Repositories
{
    public interface IProducts
    {
        //gets all products
        IEnumerable<Product> GetAll();

        //finds all products matching the specified name
        IEnumerable<Product> Find(string name);

        //gets the project that matches the specified ID - ID is a GUID
        Product Find(Guid id);

        //creates a new product
        int Add(Product item);

        //updates a product
        int Update(Product item);

        //deletes a product and its options
        int Delete(Guid id);
    }
}