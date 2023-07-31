using AutoMapper;
using fullhdfilmcenneti_core.DTOs.RoleDTOs;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using fullhdfilmcenneti_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region USER
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserWithRoleDto>();
            #endregion

            #region ROLE
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleWithUsersDto>();
            #endregion
        }
    }
}
