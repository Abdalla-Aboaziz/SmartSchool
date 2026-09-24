using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Instructor.Commands;
using SmartSchool.Application.Features.Instructor.Queries;

namespace SmartSchool.Api.Controllers
{

    [Route("api/instructors")]
    public class InstructorController : AppBaseController
    {
        public InstructorController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetInstructors()
        {
            var instructors = await _mediator.Send(new GetInstructorsListQuery());
            return NewResult(instructors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInstructorById([FromRoute] int id)
        {
            {
                var instructor = await _mediator.Send(new GetInstructorByIdQuery(id));
                return NewResult(instructor);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInstructor([FromBody] AddInstructorCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditInstructor([FromBody] EditInstructorCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInstructor([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteInstructorCommand { Id = id });
            return NewResult(result);
        }
    }
}

