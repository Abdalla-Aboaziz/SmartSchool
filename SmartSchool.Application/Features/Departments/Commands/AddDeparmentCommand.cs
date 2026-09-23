using MediatR;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class AddDeparmentCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }


    }
}
