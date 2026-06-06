using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWaveLite.Api.Models.DTOs.Auth;

namespace ShopWaveLite.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task RevokeTokenAsync(Guid userId);
    }
}