using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class EditDepartmentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }
}
