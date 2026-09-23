using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Departments.Responses;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Departments.Queries
{
    public class GetDepartmentsListQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentsListQuery, Response<List<GetDepartmentsListResponse>>>
    {
        private readonly IDepartmentRepository _studentRepository;

        public GetDepartmentsListQueryHandler(IDepartmentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Response<List<GetDepartmentsListResponse>>> Handle(GetDepartmentsListQuery request, CancellationToken cancellationToken)
        {
            var departments = await _studentRepository.GetDepartmentsListAsync();
            var response = Success(departments.Adapt<List<GetDepartmentsListResponse>>());
            response.Meta = new
            {
                TotalCount = departments.Count
            };
            return response;
        }

    }

}
