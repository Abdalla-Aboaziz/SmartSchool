using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;

namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentsPaginatedListResponse>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;


    }
}
