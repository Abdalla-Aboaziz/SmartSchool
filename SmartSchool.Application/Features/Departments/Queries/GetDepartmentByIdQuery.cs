using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Departments.Responses;

namespace SmartSchool.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {


        public int DepartmentId { get; set; }
        public int StudentPageNumber { get; set; } = 1;
        public int StudentPageSize { get; set; } = 10;
    }
}
