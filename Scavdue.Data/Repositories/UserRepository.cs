using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }

    public async Task<User> GetUserAsync(string login, string password)
    {
        var result = (await GetList(new UserSpecification(login, password))).FirstOrDefault();
        return result;
    }
    public async Task<bool> GetUserAsync(string login)
    {
        var result = (await GetList(new UserSpecification(login))).Any();
        return result;
    }

    public async Task<User> GetTokenAsync(string token)
    {
        var result = (await GetList(new TokenSpecification(token))).FirstOrDefault();
        return result;
    }
}
