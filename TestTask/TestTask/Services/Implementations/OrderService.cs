using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    /// <summary>
    /// Implementation the <see cref="IOrderService">.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Order> _orders;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
            _orders = _context.Orders;
        }

        /// <summary>
        /// Get the <see cref="Order"> with maximum sum.
        /// </summary>
        /// <returns>The <see cref="Order"></returns>
        public async Task<Order> GetOrder()=>
            await _orders
                .Where(_ => (_.Price*_.Quantity)==_orders.Max(_ => _.Price*_.Quantity))
                .FirstOrDefaultAsync();

        public Task<List<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
