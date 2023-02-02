using NuGet.Versioning;
using TheMovingCompanyAPI.Data;
using TheMovingCompanyAPI.Entities;
using TheMovingCompanyAPI.Models;

namespace TheMovingCompanyAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly DataContext _context;

        public OrderService(ILogger<OrderService> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orderDTOs = (from o in _context.Orders
                             join c in _context.Customers on o.CustomerId equals c.Id
                             select new OrderDTO
                             {
                                 Id = o.Id,
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.Name,
                                 CustomerEmail = c.Email,
                                 CustomerPhone = c.PhoneNumber,
                                 Date = o.Date,
                                 Note = o.Note,
                                 Products = (from op in _context.OrderProducts
                                             join p in _context.Products on op.ProductId equals p.Id
                                             where op.OrderId == o.Id
                                             select new ProductDTO
                                             {
                                                 Id = op.Id,
                                                 ProductTypeId = op.ProductId,
                                                 Date = op.Date,
                                                 FromAddress = op.FromAddress,
                                                 ToAddress = op.ToAddress,
                                             }
                                             ).ToList()
                             }).ToList();
            return orderDTOs;
        }

        public void CreateOrder(OrderDTO orderDTO)
        {
            var customer = new Customer
            {
                Email = orderDTO.CustomerEmail,
                Name = orderDTO.CustomerName,
                PhoneNumber = orderDTO.CustomerPhone,
            };
            _context.Customers.Add(customer);
            //_context.SaveChanges();

            var order = new Order
            {
                CustomerId = customer.Id,
                Date = orderDTO.Date,
                Note = orderDTO.Note,
            };
            _context.Orders.Add(order);
            //_context.SaveChanges();

            var orderProducts = new List<OrderProduct>();
            orderDTO.Products.ForEach(product =>
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.ProductTypeId,
                    Date = product.Date,
                    FromAddress = product.FromAddress,
                    ToAddress = product.ToAddress,
                };
                orderProducts.Add(orderProduct);
            });
            _context.OrderProducts.AddRange(orderProducts);
            _context.SaveChanges();
        }

        public void UpdateOrder(OrderDTO orderDTO, int id)
        {
            var customer = new Customer
            {
                Id = orderDTO.CustomerId,
                Email = orderDTO.CustomerEmail,
                Name = orderDTO.CustomerName,
                PhoneNumber = orderDTO.CustomerPhone,
            };

            var order = new Order
            {
                Id = orderDTO.Id,
                CustomerId = customer.Id,
                Date = orderDTO.Date,
                Note = orderDTO.Note,
            };

            var orderProducts = new List<OrderProduct>();
            //var existingOrderProducts = _context.OrderProducts.Where(op => op.OrderId == id).ToList();
            orderDTO.Products.ForEach(product =>
            {
                var orderProduct = new OrderProduct
                {
                    Id = product.Id,
                    OrderId = order.Id,
                    ProductId = product.ProductTypeId,
                    Date = product.Date,
                    FromAddress = product.FromAddress,
                    ToAddress = product.ToAddress,
                };
                orderProducts.Add(orderProduct);
            });
            //if (existingOrderProducts.Count > orderProducts.Count)
            //{
            //    var orderProductIds = orderProducts.Select(op => op.Id);
            //    var orderProducsToDelete = existingOrderProducts.Where(eop => !orderProductIds.Contains(eop.Id));
            //    _context.OrderProducts.RemoveRange(orderProducsToDelete);
            //    _context.SaveChanges();
            //}
            _context.Customers.Update(customer);
            _context.Orders.Update(order);
            _context.OrderProducts.UpdateRange(orderProducts);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = GetOrder(orderId);

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        private Order GetOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) throw new KeyNotFoundException($"Order with id {id} not found");
            return order;
        }
    }
}
