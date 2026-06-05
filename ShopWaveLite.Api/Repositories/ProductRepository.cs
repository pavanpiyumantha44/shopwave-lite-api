using Microsoft.EntityFrameworkCore;
using ShopWaveLite.Api.Data;
using ShopWaveLite.Api.Models.Entities;
using ShopWaveLite.Api.Repositories.Interfaces;

namespace ShopWaveLite.Api.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByVendorIdAsync(Guid vendorId)
        => await _dbSet
            .Where(p => p.VendorId == vendorId && p.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        => await _dbSet
            .Where(p => p.IsActive)
            .Include(p => p.Vendor)
            .ToListAsync();
}