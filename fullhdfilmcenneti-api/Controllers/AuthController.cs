using AutoMapper;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fullhdfilmcenneti_api.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IAuthService authService, IMapper mapper)
        {
            _configuration = configuration;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto request)
        {
            var result = _authService.Register(request);
            return CreateActionResult(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var result = _authService.Login(request);
            return CreateActionResult(result);
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(string request)
        {
            var result = _authService.RefreshToken(request);
            return CreateActionResult(result);
        }
    }
}
