using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Authentication.User.Command
{
    public class DeleteUserCommandHandler : ResponseHandler, IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer _stringLocalizer;

        public DeleteUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDeleteUser]);

            return Deleted<string>(_stringLocalizer[SharedResourcesKeys.UserDeletedSuccessfully]);
        }
    }
}
