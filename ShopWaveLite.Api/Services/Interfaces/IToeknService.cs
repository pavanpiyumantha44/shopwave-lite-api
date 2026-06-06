using ShopWaveLite.Api.Models.Entities;

namespace ShopWaveLite.Api.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    int RefreshTokenExpiryDays { get; }
}