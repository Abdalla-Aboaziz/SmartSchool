using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Authentication.User.Command
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
    }
}
