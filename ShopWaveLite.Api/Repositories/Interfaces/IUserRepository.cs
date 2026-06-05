using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWaveLite.Api.Models.Entities;

namespace ShopWaveLite.Api.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}