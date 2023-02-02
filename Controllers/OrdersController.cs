using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Filters;
using TheMovingCompanyAPI.Models;
using TheMovingCompanyAPI.Services;

namespace TheMovingCompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;


        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet(Name = "GetOrders")]
        public IEnumerable<OrderDTO> Get()
        {
            return _orderService.GetOrders().ToList();
        }

        [Authorize]
        [HttpPost(Name = "CreateOrder")]
        public ActionResult Post([FromBody] OrderDTO order)
        {
            try
            {
                _orderService.CreateOrder(order);
                return Ok(new { message = "Order created" });

            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"Error: {e.Message}, Exception: {e.InnerException?.Message}" });

            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "UpdateOrder")]
        public ActionResult Put(int id, [FromBody] OrderDTO order)
        {
            try
            {
                _orderService.UpdateOrder(order, id);
                return Ok(new { message = "Order updated" });

            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"Error: {e.Message}, Exception: {e.InnerException?.Message}" });
            }
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteOrder")]
        public ActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return Ok(new { message = "Order deleted" });
        }
    }
}
