using fullhdfilmcenneti_core.DTOs.UserDTOs;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.Services
{
    public interface IUserService : IService<User, UserDto>
    {
        Task<CustomResponseDto<List<UserWithRoleDto>>> GetUsersWithRole();
    }
}
