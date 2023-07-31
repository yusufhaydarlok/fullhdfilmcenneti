using fullhdfilmcenneti_core.Models;
using fullhdfilmcenneti_core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetUsersWithRole()
        {
            return await _context.Users.Include(x => x.Role).ToListAsync();
        }
    }
}
