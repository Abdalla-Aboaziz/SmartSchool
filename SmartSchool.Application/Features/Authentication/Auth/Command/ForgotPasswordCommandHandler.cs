using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;
using System.Text;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class ForgotPasswordCommandHandler : ResponseHandler, IRequestHandler<ForgotPasswordCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForgotPasswordCommand> _logger;

        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer,
            ILogger<ForgotPasswordCommand> logger) : base(stringLocalizer)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return Success<string>(string.Empty);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

#if DEBUG
            _logger.LogInformation("Password reset token for {email}: {token}", request.Email, encoded);
#endif

            // send email with token link - left to implement by caller
            return Success<string>(encoded);
        }
    }
}
