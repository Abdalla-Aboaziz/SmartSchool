using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class ForgotPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
