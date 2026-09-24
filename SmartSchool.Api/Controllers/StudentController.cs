using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Students.Commands;
using SmartSchool.Application.Features.Students.Queries;



namespace SmartSchool.Api.Controllers
{

    [Route("api/students")]
    public class StudentController : AppBaseController
    {
        public StudentController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _mediator.Send(new GetStudentsListQuery());
            return NewResult(students);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetStudentsPaginatedList([FromQuery] GetStudentPaginatedListQuery query)
        {
            var paginatedList = await _mediator.Send(query);
            return Ok(paginatedList);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var student = await _mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(student);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = id });
            return NewResult(result);
        }
    }
}
