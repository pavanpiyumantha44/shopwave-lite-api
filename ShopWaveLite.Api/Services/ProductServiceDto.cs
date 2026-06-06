using ShopWaveLite.Api.Models.DTOs.Product;
using ShopWaveLite.Api.Models.Entities;
using ShopWaveLite.Api.Repositories.Interfaces;
using ShopWaveLite.Api.Services.Interfaces;

namespace ShopWaveLite.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllActiveAsync()
    {
        var products = await _productRepository.GetActiveProductsAsync();
        return products.Select(MapToDto);
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product is null ? null : MapToDto(product);
    }

    public async Task<ProductResponseDto> CreateAsync(Guid vendorId, ProductRequestDto request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            VendorId = vendorId
        };

        var created = await _productRepository.CreateAsync(product);
        return MapToDto(created);
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid productId, Guid vendorId, ProductRequestDto request)
    {
        var product = await _productRepository.GetByIdAsync(productId)
            ?? throw new KeyNotFoundException("Product not found.");

        if (product.VendorId != vendorId)
            throw new UnauthorizedAccessException("You can only update your own products.");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        product.UpdatedAt = DateTime.UtcNow;

        var updated = await _productRepository.UpdateAsync(product);
        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid productId, Guid vendorId)
    {
        var product = await _productRepository.GetByIdAsync(productId)
            ?? throw new KeyNotFoundException("Product not found.");

        if (product.VendorId != vendorId)
            throw new UnauthorizedAccessException("You can only delete your own products.");

        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateAsync(product); // soft delete
    }

    private static ProductResponseDto MapToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        StockQuantity = p.StockQuantity,
        VendorName = p.Vendor?.FullName ?? string.Empty,
        CreatedAt = p.CreatedAt
    };
}