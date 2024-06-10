using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> _repository;

        public OrdersController(IRepository<Order> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var Orders = await _repository.GetAllAsync();
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var Order = await _repository.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound();
            }
            return Ok(Order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order Order)
        {
            await _repository.AddAsync(Order);
            return CreatedAtAction(nameof(GetOrder), new { id = Order.Id }, Order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order Order)
        {
            if (id != Order.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(Order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }

}
