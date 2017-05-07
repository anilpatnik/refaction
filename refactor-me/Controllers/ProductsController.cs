using refactor_me.Models;
using refactor_me.Repositories;
using System;
using System.Web.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProducts _products;
        private IProductOptions _productOptions;

        public ProductsController(IProducts products, IProductOptions productOptions)
        {
            _products = products;
            _productOptions = productOptions;
        }

        //Error handler with return results
        //returns ok when success else returns bad request
        public IHttpActionResult Error<T>(Func<T> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET /products - gets all products
        [Route]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Error(() => _products.GetAll());
        }

        //GET /products?name={name} - finds all products matching the specified name
        [Route]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            return Error(() => _products.Find(name));
        }

        //GET /products/{id} - gets the project that matches the specified ID - ID is a GUID
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {
            return Error(() => _products.Find(id));
        }

        //POST /products - creates a new product
        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            return Error(() => _products.Add(product));
        }

        //PUT /products/{id} - updates a product
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, Product product)
        {
            product.Id = id;
            return Error(() => _products.Update(product));
        }

        //DELETE /products/{id} - deletes a product and its options
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            return Error(() => _products.Delete(id));
        }

        //GET /products/{id}/options - finds all options for a specified product
        [Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
            return Error(() => _productOptions.GetAll(productId));
        }

        //GET /products/{id}/options/{optionId} - finds the specified product option for the specified product
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            return Error(() => _productOptions.Find(productId, id));
        }

        //POST /products/{id}/options - adds a new product option to the specified product
        [Route("{productId}/options")]
        [HttpPost]
        public IHttpActionResult CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            return Error(() => _productOptions.Add(option));
        }

        //PUT /products/{id}/options/{optionId} - updates the specified product option
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOption(Guid id, ProductOption option)
        {
            option.Id = id;
            return Error(() => _productOptions.Update(option));
        }

        //DELETE /products/{id}/options/{optionId} - deletes the specified product option
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid id)
        {
            return Error(() => _productOptions.Delete(id));
        }
    }
}