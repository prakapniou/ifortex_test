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
        private readonly int _testQuantity = 10;

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

        /// <summary>
        /// Get the <see cref="Order"> collection with quantity more than definite value.
        /// </summary>
        /// <returns>The <see cref="Order"> collection.</returns>
        public async Task<List<Order>> GetOrders()=>
            await _orders
                .Where(_ => _.Quantity>_testQuantity)
                .ToListAsync();
    }
}
