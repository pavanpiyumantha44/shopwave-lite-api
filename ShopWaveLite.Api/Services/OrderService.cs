using ShopWaveLite.Api.Models.DTOs.Order;
using ShopWaveLite.Api.Models.Entities;
using ShopWaveLite.Api.Repositories.Interfaces;
using ShopWaveLite.Api.Services.Interfaces;

namespace ShopWaveLite.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderResponseDto> CreateAsync(Guid customerId, CreateOrderRequestDto request)
    {
        var orderItems = new List<OrderItem>();
        decimal total = 0;

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId)
                ?? throw new KeyNotFoundException($"Product {item.ProductId} not found.");

            if (!product.IsActive)
                throw new InvalidOperationException($"Product '{product.Name}' is no longer available.");

            if (product.StockQuantity < item.Quantity)
                throw new InvalidOperationException($"Insufficient stock for '{product.Name}'.");

            product.StockQuantity -= item.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price  // price snapshot
            };

            orderItems.Add(orderItem);
            total += product.Price * item.Quantity;
        }

        var order = new Order
        {
            CustomerId = customerId,
            TotalAmount = total,
            OrderItems = orderItems
        };

        var created = await _orderRepository.CreateAsync(order);
        return MapToDto(created);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetByCustomerAsync(Guid customerId)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
        return orders.Select(MapToDto);
    }

    public async Task<OrderResponseDto?> GetByIdAsync(Guid orderId)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
        return order is null ? null : MapToDto(order);
    }

    public async Task CancelAsync(Guid orderId, Guid customerId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId)
            ?? throw new KeyNotFoundException("Order not found.");

        if (order.CustomerId != customerId)
            throw new UnauthorizedAccessException("You can only cancel your own orders.");

        if (order.Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be cancelled.");

        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.UpdateAsync(order);
    }

    private static OrderResponseDto MapToDto(Order o) => new()
    {
        Id = o.Id,
        Status = o.Status.ToString(),
        TotalAmount = o.TotalAmount,
        CreatedAt = o.CreatedAt,
        Items = o.OrderItems.Select(oi => new OrderItemResponseDto
        {
            ProductId = oi.ProductId,
            ProductName = oi.Product?.Name ?? string.Empty,
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice
        }).ToList()
    };
}