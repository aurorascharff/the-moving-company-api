using TheMovingCompanyAPI.Data;
using TheMovingCompanyAPI.Entities;

namespace TheMovingCompanyAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly DataContext _context;

        public ProductService(ILogger<ProductService> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }
    }
}
