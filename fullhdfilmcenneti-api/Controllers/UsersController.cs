using AutoMapper;
using fullhdfilmcenneti_api.Filters;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fullhdfilmcenneti_core.Services;

namespace fullhdfilmcenneti_api.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsersWithRole()
        {
            return CreateActionResult(await _userService.GetUsersWithRole());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _userService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, users));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, user));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateUserDto userDto)
        {
            var user = await _userService.AddAsync(userDto);
            return CreateActionResult(CustomResponseDto<CreateUserDto>.Success(201, usersDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            await _userService.UpdateAsync(_mapper.Map<User>(userDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _userService.RemoveAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
