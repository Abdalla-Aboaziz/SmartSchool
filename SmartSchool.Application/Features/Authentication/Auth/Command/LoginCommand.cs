using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.Auth.Responses;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class LoginCommand : IRequest<Response<LoginResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
