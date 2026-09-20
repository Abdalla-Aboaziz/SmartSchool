using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

    }
}
