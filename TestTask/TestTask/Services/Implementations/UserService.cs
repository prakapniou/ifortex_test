using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    /// <summary>
    /// Implementation the <see cref="IUserService">.
    /// </summary>
    public sealed class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;
        private readonly DbSet<Order> _orders;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
            _users = _context.Users;
            _orders = _context.Orders;
        }

        /// <summary>
        /// Get the <see cref="User"> with max orders count.
        /// </summary>
        /// <returns>The <see cref="User"></returns>
        public Task<User> GetUser()
        {
            // Create grouped by user identifier order collection with userId-key & Count-value.
            var ordersGroupByUserId = _orders
                .GroupBy(_ => _.UserId)
                .Select(group => new
                {
                    UserId = group.Key,
                    Count = group.Count()
                });

            // Get from grouped by user identifier order collection userId with maximum count.
            var userIdWithMaxOrders = ordersGroupByUserId
                    .Where(_ => _.Count==ordersGroupByUserId.Max(_ => _.Count))
                    .Select(_ => _.UserId)
                    .FirstOrDefault();

            // Get user by identifier from user collection.
            var userWithMaxOrders = _users.Where(_ => _.Id==userIdWithMaxOrders).FirstOrDefaultAsync();

            return userWithMaxOrders;
        }

        /// <summary>
        /// Get the <see cref="User"> collection with inactive <see cref="UserStatus">
        /// </summary>
        /// <returns>The <see cref="User"></returns>
        public async Task<List<User>> GetUsers()=>
            await _users
                .Where(_ => _.Status == UserStatus.Inactive)
                .ToListAsync();
    }
}
