using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.User.Responses;

namespace SmartSchool.Application.Features.Authentication.User.Queries
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<UserListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
