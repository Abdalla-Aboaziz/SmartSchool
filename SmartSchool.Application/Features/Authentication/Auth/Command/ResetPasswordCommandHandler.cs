using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;
using System.Text;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class ResetPasswordCommandHandler : ResponseHandler, IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);

            var decoded = WebEncoders.Base64UrlDecode(request.Token);
            var token = Encoding.UTF8.GetString(decoded);

            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);

            // revoke refresh tokens
            foreach (var rt in user.RefreshTokens)
            {
                rt.RevokedOn = DateTime.UtcNow;
            }

            await _userManager.UpdateAsync(user);

            return Success<string>(string.Empty);
        }
    }
}
