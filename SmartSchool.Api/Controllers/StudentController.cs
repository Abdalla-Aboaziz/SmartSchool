using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Students.Commands;
using SmartSchool.Application.Features.Students.Queries;



namespace SmartSchool.Api.Controllers
{

    public class StudentController : AppBaseController
    {
        public StudentController()
        {

        }

        [HttpGet("Student/List")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _mediator.Send(new GetStudentsListQuery());
            return NewResult(students);
        }
        [HttpGet("Student/{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var student = await _mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(student);
        }
        [HttpPost("Student/Create")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut("Student/Edit")]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("Student/{id:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = id });
            return NewResult(result);
        }
    }
}
