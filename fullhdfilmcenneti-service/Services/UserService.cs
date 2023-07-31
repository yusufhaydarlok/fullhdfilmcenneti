using AutoMapper;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using fullhdfilmcenneti_core.Repositories;
using fullhdfilmcenneti_core.Services;
using fullhdfilmcenneti_core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_service.Services
{
    public class UserService : Service<User, UserDto>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CustomResponseDto<List<UserWithRoleDto>>> GetUsersWithRole()
        {
            var users = await _userRepository.GetUsersWithRole();
            var usersDto = _mapper.Map<List<UserWithRoleDto>>(users);
            return CustomResponseDto<List<UserWithRoleDto>>.Success(200, usersDto);
        }
    }
}
