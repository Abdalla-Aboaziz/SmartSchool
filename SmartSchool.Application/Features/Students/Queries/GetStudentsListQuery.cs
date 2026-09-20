using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;

namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentsListQuery : IRequest<Response<List<GetStudentsListResponse>>>
    {

    }
}
