using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var Products = await _repository.GetAllAsync();
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var Product = await _repository.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product Product)
        {
            await _repository.AddAsync(Product);
            return CreatedAtAction(nameof(GetProduct), new { id = Product.Id }, Product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product Product)
        {
            if (id != Product.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(Product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }

}
