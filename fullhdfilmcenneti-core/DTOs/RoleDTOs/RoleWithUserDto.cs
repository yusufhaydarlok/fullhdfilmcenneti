using fullhdfilmcenneti_core.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.DTOs.RoleDTOs
{
    public class RoleWithUsersDto : RoleDto
    {
        public List<UserDto> Users { get; set; }
    }
}
