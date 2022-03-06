using log4net;
using refactor_me.Models;
using refactor_me.Repositories;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private ILog _log;
        private IProducts _products;
        private IProductOptions _productOptions;

        public ProductsController(ILog log, IProducts products, IProductOptions productOptions)
        {
            _log = log;
            _products = products;
            _productOptions = productOptions;
        }

        //Error handler with return results
        //returns ok when success else returns not found
        public async Task<IHttpActionResult> Error<T>(Func<T> func)
        {
            try
            {
                return await Task.Run(() => Ok(func()));
            }
            catch (Exception ex)
            {
                _log.Error(ex); //logs to the file

                return await Task.Run(() => NotFound());
            }
        }

        //GET /products - gets all products
        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            return await Error(() => _products.GetAll());
        }

        //GET /products?name={name} - finds all products matching the specified name
        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> SearchByName(string name)
        {
            return await Error(() => _products.Find(name));
        }

        //GET /products/{id} - gets the project that matches the specified ID - ID is a GUID
        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {
            return await Error(() => _products.Find(id));
        }

        //POST /products - creates a new product
        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> Create(Product product)
        {
            return await Error(() => _products.Add(product));
        }

        //PUT /products/{id} - updates a product
        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, Product product)
        {
            product.Id = id;
            return await Error(() => _products.Update(product));
        }

        //DELETE /products/{id} - deletes a product and its options
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            return await Error(() => _products.Delete(id));
        }

        //GET /products/{id}/options - finds all options for a specified product
        [Route("{productId}/options")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOptions(Guid productId)
        {
            return await Error(() => _productOptions.GetAll(productId));
        }

        //GET /products/{id}/options/{optionId} - finds the specified product option for the specified product
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOption(Guid productId, Guid id)
        {
            return await Error(() => _productOptions.Find(productId, id));
        }

        //POST /products/{id}/options - adds a new product option to the specified product
        [Route("{productId}/options")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            return await Error(() => _productOptions.Add(option));
        }

        //PUT /products/{id}/options/{optionId} - updates the specified product option
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOption(Guid id, ProductOption option)
        {
            option.Id = id;
            return await Error(() => _productOptions.Update(option));
        }

        //DELETE /products/{id}/options/{optionId} - deletes the specified product option
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteOption(Guid id)
        {
            return await Error(() => _productOptions.Delete(id));
        }
    }
}