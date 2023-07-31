using AutoMapper;
using fullhdfilmcenneti_core.DTOs.RoleDTOs;
using fullhdfilmcenneti_core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fullhdfilmcenneti_api.Controllers
{
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            var rolesDto = _mapper.Map<List<RoleDto>>(roles.ToList());
            return CreateActionResult(CustomResponseDto<List<RoleDto>>.Success(200, rolesDto));
        }

        [HttpGet("[action]/{roleId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSingleRoleByIdWithUsersAsync(Guid roleId)
        {
            return CreateActionResult(await _roleService.GetSingleRoleByIdWithUsersAsync(roleId));
        }
    }
}
