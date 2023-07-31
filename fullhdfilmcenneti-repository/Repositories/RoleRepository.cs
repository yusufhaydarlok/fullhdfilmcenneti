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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Role> GetSingleRoleByIdWithUsersAsync(Guid roleId)
        {
            return await _context.Roles.Include(x => x.Users).Where(x => x.Id == roleId).SingleOrDefaultAsync();
        }
    }
}
