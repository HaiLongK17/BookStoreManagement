using AutoMapper;
using BookStoreManagement.Core;
using BookStoreManagement.Core.Constants;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Enums;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Core.Wrappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStoreManagement.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService,
            IPhotoAccessor photoAccessor, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            _photoAccessor = photoAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response<UserDto>> GetCurrentUser(CookieDto cookie)
        {
            List<string> errors = new();

            var user = await _unitOfWork.Users.GetUserWithRefreshTokens(cookie.Email);

            if(user == null)
            {
                errors.Add(Errors.GET_USER_FAILURE);
                return Response<UserDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == cookie.RefreshToken);

            if (oldToken == null || (oldToken != null && !oldToken.IsActive))
            {
                errors.Add(Errors.BAD_REFRESH_TOKEN);
                return Response<UserDto>.Failure(Messages.UNAUTHORIZED, errors);
            }

            var role = await _userManager.GetRolesAsync(user);

            var result = _mapper.Map<UserDto>(user);

            result.Role = string.Join("; ", role);

            return Response<UserDto>.Success((int)HttpStatusCode.OK, result);
        }

        public async Task<Response<CookieDto>> Login(LoginDto loginDto)
        {
            List<string> errors = new();

            var user = await _unitOfWork.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Account);

            if (user == null)
            {
                user = await _unitOfWork.Users.SingleOrDefaultAsync(x => x.PhoneNumber == loginDto.Account);
                if (user == null)
                {
                    errors.Add(Errors.INCORRECT_ACCOUNT);
                    return Response<CookieDto>.Failure(Messages.BAD_REQUEST, errors);
                }
            }

            if (!user.Status)
            {
                errors.Add("Account is currently locked. Please contact to site owner");
                return Response<CookieDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, true);

            if (!loginResult.Succeeded)
            {
                errors.Add(Errors.INCORRECT_PASSWORD);
                return Response<CookieDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var refreshToken = await CreateRefreshToken(user, loginDto.RememberMe);

            var result = CreateUserObject(user, refreshToken.Token);

            result.ExpireTime = refreshToken.Expires;

            return Response<CookieDto>.Success((int)HttpStatusCode.OK, result);
        }

        public async Task<Response<CookieDto>> RefreshToken(CookieDto cookie)
        {
            List<string> errors = new();

            var user = await _unitOfWork.Users.GetUserWithRefreshTokens(cookie.Email);

            if (user == null)
            {
                errors.Add(Errors.GET_USER_FAILURE);
                return Response<CookieDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == cookie.RefreshToken);

            if (oldToken == null || (oldToken != null && !oldToken.IsActive))
            {
                errors.Add(Errors.BAD_REFRESH_TOKEN);
                return Response<CookieDto>.Failure(Messages.UNAUTHORIZED, errors);
            }

            var result = CreateUserObject(user, cookie.RefreshToken);

            result.ExpireTime = oldToken.Expires;

            return Response<CookieDto>.Success((int)HttpStatusCode.OK, result);
        }

        public async Task<Response<CookieDto>> Register(RegisterDto registerDto)
        {
            List<string> errors = new();

            var user = _mapper.Map<User>(registerDto);

            var uploadResult = await _photoAccessor.AddPhoto(null, Applications.USER);

            user.Avatar = "https://img.freepik.com/free-vector/flat-design-library-logo-template_23-2149325326.jpg?w=2000";
            user.AvatarPublicId = uploadResult.PublicId;

            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var addResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (!addResult.Succeeded)
            {
                errors.Add(Errors.BAD_ACCOUNT);
                return Response<CookieDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            await _userManager.AddToRoleAsync(user, Roles.NormalUser.ToString());

            await _userManager.SetLockoutEnabledAsync(user, false);

            var refreshToken = await CreateRefreshToken(user);

            var result = CreateUserObject(user, refreshToken.Token);

            result.ExpireTime = refreshToken.Expires;

            return Response<CookieDto>.Success((int)HttpStatusCode.OK, result);
        }

        private CookieDto CreateUserObject(User user, string refreshToken)
        {
            return new CookieDto
            {
                Token = _tokenService.CreateToken(user),
                RefreshToken = refreshToken,
                Email = user.Email
            };
        }
        private async Task<RefreshToken> CreateRefreshToken(User user, bool isStayed = false)
        {
            var refreshToken = _tokenService.GenerateRefreshToken(isStayed);

            user.RefreshTokens.Add(refreshToken);

            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveAsync();

            return refreshToken;
        }
    }
}
