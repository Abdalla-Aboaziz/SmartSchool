using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Departments.Commands;
using SmartSchool.Application.Features.Departments.Queries;

namespace SmartSchool.Api.Controllers
{

    [Route("api/departments")]
    public class DepartmentController : AppBaseController
    {
        public DepartmentController()
        {

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var department = await _mediator.Send(query);
            return NewResult(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _mediator.Send(new GetDepartmentsListQuery());
            return NewResult(departments);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] AddDeparmentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("assign-manager")]
        public async Task<IActionResult> AssignDepartmentManager([FromBody] AssignDepartmentManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditDepartment([FromBody] EditDepartmentCommand command)
        {

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteDepartmentCommand { Id = id });
            return NewResult(result);
        }

    }
}
