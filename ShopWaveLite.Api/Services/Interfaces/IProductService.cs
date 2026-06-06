using ShopWaveLite.Api.Models.DTOs.Product;

namespace ShopWaveLite.Api.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllActiveAsync();
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<ProductResponseDto> CreateAsync(Guid vendorId, ProductRequestDto request);
    Task<ProductResponseDto> UpdateAsync(Guid productId, Guid vendorId, ProductRequestDto request);
    Task DeleteAsync(Guid productId, Guid vendorId);
}