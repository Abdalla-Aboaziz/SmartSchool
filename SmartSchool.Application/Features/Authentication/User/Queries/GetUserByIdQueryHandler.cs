using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.User.Responses;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Authentication.User.Queries
{
    class GetUserByIdQueryHandler : ResponseHandler, IRequestHandler<GetUserByIdQuery, Response<GetUserByIdUserResponse>>
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<ApplicationUser> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<Response<GetUserByIdUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return NotFound<GetUserByIdUserResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }

            var response = new GetUserByIdUserResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };

            return Success<GetUserByIdUserResponse>(response);
        }
    }
}
