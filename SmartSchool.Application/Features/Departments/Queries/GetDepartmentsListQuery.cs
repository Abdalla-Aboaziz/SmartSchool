using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Departments.Responses;

namespace SmartSchool.Application.Features.Departments.Queries
{
    public class GetDepartmentsListQuery : IRequest<Response<List<GetDepartmentsListResponse>>>
    {

    }
}
