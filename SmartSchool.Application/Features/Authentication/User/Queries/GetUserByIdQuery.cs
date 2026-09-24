using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.User.Responses;

namespace SmartSchool.Application.Features.Authentication.User.Queries
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdUserResponse>>
    {
        public Guid UserId { get; set; }

    }
}
