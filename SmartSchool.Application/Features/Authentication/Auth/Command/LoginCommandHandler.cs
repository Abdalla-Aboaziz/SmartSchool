using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.Auth.Responses;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Application.Services;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class LoginCommandHandler : ResponseHandler, IRequestHandler<LoginCommand, Response<LoginResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer,
            ITokenService tokenService) : base(stringLocalizer)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Response<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user == null)
                return Unauthorized<LoginResponse>();

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                return Unauthorized<LoginResponse>();

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresOn,
                UserId = user.Id
            };

            return Success(response);
        }
    }
}
