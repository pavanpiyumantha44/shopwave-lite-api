using Microsoft.EntityFrameworkCore;
using ShopWaveLite.Api.Data;
using ShopWaveLite.Api.Models.Entities;
using ShopWaveLite.Api.Repositories.Interfaces;

namespace ShopWaveLite.Api.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<bool> EmailExistsAsync(string email)
        => await _dbSet.AnyAsync(u => u.Email == email.ToLower());
}