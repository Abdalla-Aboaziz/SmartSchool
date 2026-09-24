using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;
using System.Text;

namespace SmartSchool.Application.Features.Authentication.User.Command
{
    public class AddUserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly ILogger<AddUserCommand> _logger;


        public AddUserCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer,
            ILogger<AddUserCommand> logger
                                               ) : base(stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _logger = logger;

        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailIsExists)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailAlreadyExists]);



            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Generate Code 

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


#if DEBUG
                _logger.LogInformation("Confirmation Code: {code}", code);
#endif

                // Send Email 

                //   await SendConfirmationEmailAsync(user, code);

                return Created(user.Id);
            }

            foreach (var e in result.Errors)
            {
                _logger.LogInformation("{Code} - {Description}",
                                            e.Code,
                                            e.Description);
            }

            var error = result.Errors.First();

            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddUser]);

        }


    }
}
