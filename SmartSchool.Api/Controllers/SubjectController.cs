using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Subject.Commands;
using SmartSchool.Application.Features.Subject.Queries;

namespace SmartSchool.Api.Controllers
{

    [Route("api/subjects")]
    public class SubjectController : AppBaseController
    {
        public SubjectController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _mediator.Send(new GetSubjectsListQuery());
            return NewResult(subjects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubjectById([FromRoute] int id)
        {
            var subject = await _mediator.Send(new GetSubjectByIdQuery(id));
            return NewResult(subject);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] AddSubjectCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditSubject([FromBody] EditSubjectCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSubjectCommand { Id = id });
            return NewResult(result);
        }
    }

}