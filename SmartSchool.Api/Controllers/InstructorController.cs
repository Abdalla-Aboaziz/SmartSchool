using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Instructor.Commands;
using SmartSchool.Application.Features.Instructor.Queries;

namespace SmartSchool.Api.Controllers
{

    public class InstructorController : AppBaseController
    {
        public InstructorController()
        {

        }

        [HttpGet("Instructor/List")]
        public async Task<IActionResult> GetInstructors()
        {
            var instructors = await _mediator.Send(new GetInstructorsListQuery());
            return NewResult(instructors);
        }

        [HttpGet("Instructor/{id:int}")]
        public async Task<IActionResult> GetInstructorById([FromRoute] int id)
        {
            {
                var instructor = await _mediator.Send(new GetInstructorByIdQuery(id));
                return NewResult(instructor);
            }
        }

        [HttpPost("Instructor/Create")]
        public async Task<IActionResult> AddInstructor([FromBody] AddInstructorCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("Instructor/Edit")]
        public async Task<IActionResult> EditInstructor([FromBody] EditInstructorCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("Instructor/{id:int}")]
        public async Task<IActionResult> DeleteInstructor([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteInstructorCommand { Id = id });
            return NewResult(result);
        }
    }
}

