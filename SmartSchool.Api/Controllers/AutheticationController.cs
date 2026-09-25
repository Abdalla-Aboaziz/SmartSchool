using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Authentication.User.Command;
using SmartSchool.Application.Features.Authentication.Auth.Command;

namespace SmartSchool.Api.Controllers
{
    [Route("api/Authetication")]
    public class AutheticationController : AppBaseController
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] AddUserCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
    }
}
