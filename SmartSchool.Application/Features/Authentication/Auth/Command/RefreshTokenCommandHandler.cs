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
    public class RefreshTokenCommandHandler : ResponseHandler, IRequestHandler<RefreshTokenCommand, Response<LoginResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer,
            ITokenService tokenService) : base(stringLocalizer)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Response<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == request.Token), cancellationToken);

            if (user == null)
                return Unauthorized<LoginResponse>();

            var existing = user.RefreshTokens.Single(rt => rt.Token == request.Token);
            if (!existing.IsActive)
                return Unauthorized<LoginResponse>();

            // revoke old
            existing.RevokedOn = DateTime.UtcNow;

            // create new
            var newRefresh = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefresh);

            await _userManager.UpdateAsync(user);

            var access = _tokenService.GenerateAccessToken(user);

            var resp = new LoginResponse
            {
                AccessToken = access,
                RefreshToken = newRefresh.Token,
                ExpiresAt = newRefresh.ExpiresOn,
                UserId = user.Id
            };

            return Success(resp);
        }
    }
}
