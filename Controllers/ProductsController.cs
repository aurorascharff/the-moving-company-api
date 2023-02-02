using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheMovingCompanyAPI.Entities;
using TheMovingCompanyAPI.Services;

namespace TheMovingCompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _serviceService;

        public ProductsController(ILogger<ProductsController> logger, IProductService orderService)
        {
            _logger = logger;
            _serviceService = orderService;
        }

        [Authorize]
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return _serviceService.GetProducts().ToList();
        }
    }
}
