using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

    }
}
