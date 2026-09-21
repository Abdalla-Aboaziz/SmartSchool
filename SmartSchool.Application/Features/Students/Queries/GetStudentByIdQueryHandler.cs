using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentByIdQueryHandler : ResponseHandler, IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        { 
            var student  = await _studentRepository.GetByIdWithDepartmentAsync(request.StudentId);
        
            if(student == null)
            {
                return NotFound<GetStudentByIdResponse>(_stringLocalizer[SharedResourcesKeys.StudentNotFound]);
            }
            return Success(student.Adapt<GetStudentByIdResponse>());
        }
        
    }
}
