using ShopWaveLite.Api.Models.Entities;

namespace ShopWaveLite.Api.Repositories.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    Task<Order?> GetOrderWithItemsAsync(Guid orderId);
}