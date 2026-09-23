using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Students.Commands
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}
