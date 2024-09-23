using Microsoft.AspNetCore.Mvc;
using OrderStateless.Models;
using OrderStateless.Service;

namespace OrderStateless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPost("{id}/pay")]
        public IActionResult PayOrder(int id)
        {
            _orderService.PayOrder(id);
            return NoContent();
        }

        [HttpPost("{id}/ship")]
        public IActionResult ShipOrder(int id)
        {
            _orderService.ShipOrder(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public IActionResult CompleteOrder(int id)
        {
            _orderService.CompleteOrder(id);
            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public IActionResult CancelOrder(int id)
        {
            _orderService.CancelOrder(id);
            return NoContent();
        }
    }
}
