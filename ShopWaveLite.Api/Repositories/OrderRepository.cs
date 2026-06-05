using Microsoft.EntityFrameworkCore;
using ShopWaveLite.Api.Data;
using ShopWaveLite.Api.Models.Entities;
using ShopWaveLite.Api.Repositories.Interfaces;

namespace ShopWaveLite.Api.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId)
        => await _dbSet
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<Order?> GetOrderWithItemsAsync(Guid orderId)
        => await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == orderId);
}