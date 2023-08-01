using fullhdfilmcenneti_core.DTOs.UserDTOs;
using fullhdfilmcenneti_core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace fullhdfilmcenneti_core.Services
{
    public interface IAuthService
    {
        Task<CustomResponseDto<UserDto>> Login(UserDto request);
        Task<CustomResponseDto<CreateUserDto>> Register(CreateUserDto request);
        Task<CustomResponseDto<string>> RefreshToken(string request);
        string GetMyId();
    }
}
