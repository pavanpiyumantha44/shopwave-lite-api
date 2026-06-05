using ShopWaveLite.Api.Models.Entities;

namespace ShopWaveLite.Api.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByVendorIdAsync(Guid vendorId);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
}