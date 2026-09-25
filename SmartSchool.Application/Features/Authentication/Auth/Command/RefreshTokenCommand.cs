using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Authentication.Auth.Responses;

namespace SmartSchool.Application.Features.Authentication.Auth.Command
{
    public class RefreshTokenCommand : IRequest<Response<LoginResponse>>
    {
        public string Token { get; set; } = string.Empty;
    }
}
