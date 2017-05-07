using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Repositories
{
    public interface IProductOptions
    {
        //finds all options for a specified product
        IEnumerable<ProductOption> GetAll(Guid productId);

        //finds the specified product option for the specified product
        ProductOption Find(Guid productId, Guid id);

        //adds a new product option to the specified product
        int Add(ProductOption item);

        //updates the specified product option
        int Update(ProductOption item);

        //deletes the specified product option
        int Delete(Guid id);
    }
}