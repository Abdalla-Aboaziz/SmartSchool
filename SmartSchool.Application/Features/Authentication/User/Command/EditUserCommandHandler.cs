using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Authentication.User.Command
{
    public class EditUserCommandHandler : ResponseHandler, IRequestHandler<EditUserCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer _stringLocalizer;

        public EditUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

            var emailExists = await _userManager.Users.AnyAsync(
                x => x.Email == request.Email && x.Id != user.Id,
                cancellationToken);

            if (emailExists)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailAlreadyExists]);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;

            if (!string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailResult = await _userManager.SetEmailAsync(user, request.Email);
                if (!emailResult.Succeeded)
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUser]);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUser]);

            return Success(user.Id, _stringLocalizer[SharedResourcesKeys.UserUpdatedSuccessfully]);
        }
    }
}
