using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
