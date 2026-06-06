using ShopWaveLite.Api.Models.DTOs.Order;

namespace ShopWaveLite.Api.Services.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> CreateAsync(Guid customerId, CreateOrderRequestDto request);
    Task<IEnumerable<OrderResponseDto>> GetByCustomerAsync(Guid customerId);
    Task<OrderResponseDto?> GetByIdAsync(Guid orderId);
    Task CancelAsync(Guid orderId, Guid customerId);
}