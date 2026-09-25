using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Authentication.User.Command
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
