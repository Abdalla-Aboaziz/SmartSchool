using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Authentication.User.Queries;

namespace SmartSchool.Api.Controllers
{
    [Route("api/User")]

    public class UserController : AppBaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetUserList([FromQuery] GetUserPaginationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { UserId = id });
            return Ok(result);
        }
    }
}
