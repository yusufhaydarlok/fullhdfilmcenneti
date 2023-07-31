using AutoMapper;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.DTOs.RoleDTOs;
using fullhdfilmcenneti_core.Models;
using fullhdfilmcenneti_core.Repositories;
using fullhdfilmcenneti_core.Services;
using fullhdfilmcenneti_core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_service.Services
{
    public class RoleService : Service<Role, RoleDto>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork, IMapper mapper, IRoleRepository roleRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<CustomResponseDto<RoleWithUsersDto>> GetSingleRoleByIdWithUsersAsync(Guid roleId)
        {
            var role = await _roleRepository.GetSingleRoleByIdWithUsersAsync(roleId);
            var roleDto = _mapper.Map<RoleWithUsersDto>(role);
            return CustomResponseDto<RoleWithUsersDto>.Success(200, roleDto);
        }
    }
}
