using TheMovingCompanyAPI.Entities;

namespace TheMovingCompanyAPI.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
    }
}
