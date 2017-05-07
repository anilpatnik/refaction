using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.Repositories
{
    public interface IProductOptions
    {
        //finds all options for a specified product
        Task<IEnumerable<ProductOption>> GetAll(Guid productId);

        //finds the specified product option for the specified product
        Task<ProductOption> Find(Guid productId, Guid id);

        //adds a new product option to the specified product
        Task<int> Add(ProductOption item);

        //updates the specified product option
        Task<int> Update(ProductOption item);

        //deletes the specified product option
        Task<int> Delete(Guid id);
    }
}