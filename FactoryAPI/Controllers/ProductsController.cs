using FactoryAPI.Models;
using FactoryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<List<Product>> GetProduct()
        {
            return await _productRepository.Get();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProduct(string Id)
        {
            var product = await _productRepository.Get(Id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> PutProduct(string Id, [FromBody] Product product)
        {
            var _product = await _productRepository.Get(Id);

            if (_product is null)
            {
                return NotFound();
            }

            product.Id = _product.Id;

            await _productRepository.Update(Id, product);

            return NoContent();
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            var product = await _productRepository.Get(Id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Remove(Id);
            return NoContent();

        }

    }
}
