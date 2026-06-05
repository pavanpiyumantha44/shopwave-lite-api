using ShopWaveLite.Api.Models.Entities;

namespace ShopWaveLite.Api.Repositories.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task RevokeAllUserTokensAsync(Guid userId);
}