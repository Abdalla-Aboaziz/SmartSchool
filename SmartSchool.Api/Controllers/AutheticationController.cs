using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Authentication.User.Command;

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
    }
}
