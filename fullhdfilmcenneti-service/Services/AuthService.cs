using AutoMapper;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using fullhdfilmcenneti_core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using fullhdfilmcenneti_core.Repositories;
using fullhdfilmcenneti_repository.Repositories;

namespace fullhdfilmcenneti_service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<User> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IMapper mapper, IGenericRepository<User> repository,IAuthService authService, IUserService userService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _userService = userService;
            _authService = authService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyId()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue("Id");
            }

            return result;
        }

        public async Task<CustomResponseDto<UserDto>> Login(UserDto request)
        {
            var user = (await _repository.FindAsync(x => x.Username == request.Username)).FirstOrDefault();
            var users = _repository.GetAll();
            var usersDto = _mapper.Map<List<UserDto>>(users.ToList());
            if (!usersDto.Any(x => x.Username == request.Username))
            {
                return CustomResponseDto<UserDto>.Fail(404 ,"User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return CustomResponseDto<UserDto>.Fail(204, "Wrong password.");
            }

            var token = CreateToken(user);
            var refreshToken = GenerateRefreshToken();
            HttpResponse httpResponse = null;
            SetRefreshToken(refreshToken, httpResponse);
            return CustomResponseDto<UserDto>.Success(200,_mapper.Map<UserDto>(token));
        }

        public async Task<CustomResponseDto<string>> RefreshToken(string request)
        {
            HttpRequest httpRequest = null;
            HttpResponse httpResponse = null;

            var userId = _authService.GetMyId();
            var user = (await _repository.FindAsync(x => x.Id.ToString() == userId)).FirstOrDefault();
            var refreshToken = httpRequest.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return CustomResponseDto<string>.Fail(401 ,"Invalid Refresh Token.");
            }

            else if (user.TokenExpires < DateTime.Now)
            {
                return CustomResponseDto<string>.Fail(401 ,"Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, httpResponse);
            return CustomResponseDto<string>.Success(200, token);
        }

        public async Task<CustomResponseDto<CreateUserDto>> Register(CreateUserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = _mapper.Map<User>(request);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userService.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            return CustomResponseDto<CreateUserDto>.Success(201, request);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                //new Claim(ClaimTypes.Role, user.RoleId == 1 ? "Admin" : "User" )
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshToken newRefreshToken, HttpResponse httpResponse)
        {
            var userId = _authService.GetMyId();
            var user = await _repository.GetByIdAsync(Guid.Parse(userId));

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            
            httpResponse.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            await _userService.UpdateAsync(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
