using TheMovingCompanyAPI.Entities;
using TheMovingCompanyAPI.Models;

namespace TheMovingCompanyAPI.Services
{
    public interface IOrderService
    {
        public void CreateOrder(OrderDTO order);
        public void UpdateOrder(OrderDTO order, int id);
        public void DeleteOrder(int orderId);

        public IEnumerable<OrderDTO> GetOrders();
    }
}
