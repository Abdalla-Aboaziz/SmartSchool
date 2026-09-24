using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.User.Responses;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Authentication.User.Queries
{
    public class GetUserListQueryHandler : ResponseHandler, IRequestHandler<GetUserPaginationQuery, PaginatedResult<UserListResponse>>
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserListQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<ApplicationUser> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }
        public async Task<PaginatedResult<UserListResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users
                    .AsNoTracking()
                      .Select(x => new UserListResponse
                      {
                          FirstName = x.FirstName,
                          LastName = x.LastName,
                          Email = x.Email,
                          Address = x.Address,
                          PhoneNumber = x.PhoneNumber
                      });

            var result = await users.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize);

            return result;
        }
    }
}
