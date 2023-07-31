using fullhdfilmcenneti_core.DTOs.RoleDTOs;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.Services
{
    public interface IRoleService : IService<Role, RoleDto>
    {
        Task<CustomResponseDto<RoleWithUsersDto>> GetSingleRoleByIdWithUsersAsync(Guid roleId);
    }
}
