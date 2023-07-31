using fullhdfilmcenneti_core.DTOs.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.DTOs.UserDTOs
{
    public class UserWithRoleDto : UserDto
    {
        public RoleDto Role { get; set; }
    }
}
