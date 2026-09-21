using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;
using System.Linq.Expressions;

namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentPaginatedListQueryHandler : ResponseHandler, IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentsPaginatedListResponse>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentPaginatedListQueryHandler(IStudentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
        }
        public async Task<PaginatedResult<GetStudentsPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentsPaginatedListResponse>> expression = student => new GetStudentsPaginatedListResponse
            {
                StudentId = student.Id,
                Name = student.Name,
                Address = student.Address,
                DepartmentName = student.Department.Name
            };

            var query = _studentRepository.GetStudentsQueryable();

            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
