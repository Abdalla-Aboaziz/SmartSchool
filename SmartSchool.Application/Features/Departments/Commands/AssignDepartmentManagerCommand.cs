using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class AssignDepartmentManagerCommand : IRequest<Response<string>>
    {
        public int DepartmentId { get; set; }
        public int InstructorId { get; set; }
    }
}
