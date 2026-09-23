using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Departments.Commands;
using SmartSchool.Application.Features.Departments.Queries;

namespace SmartSchool.Api.Controllers
{

    public class DepartmentController : AppBaseController
    {
        public DepartmentController()
        {

        }

        [HttpGet("Department/id")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var department = await _mediator.Send(query);
            return NewResult(department);
        }

        [HttpGet("Department/List")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _mediator.Send(new GetDepartmentsListQuery());
            return NewResult(departments);
        }

        [HttpPost("Department/Create")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDeparmentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("Department/AssignManager")]
        public async Task<IActionResult> AssignDepartmentManager([FromBody] AssignDepartmentManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("Department/Edit")]
        public async Task<IActionResult> EditDepartment([FromBody] EditDepartmentCommand command)
        {

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("Department/{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteDepartmentCommand { Id = id });
            return NewResult(result);
        }

    }
}
