using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Authentication.User.Command
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
