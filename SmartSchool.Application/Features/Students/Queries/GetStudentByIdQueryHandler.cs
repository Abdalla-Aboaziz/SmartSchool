using Mapster;
using MediatR;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;

namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentByIdQueryHandler : ResponseHandler, IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        { 
            var student  = await _studentRepository.GetByIdWithDepartmentAsync(request.StudentId);
        
            if(student == null)
            {
                return NotFound<GetStudentByIdResponse>($"Student with ID {request.StudentId} not found.");
            }
            return Success(student.Adapt<GetStudentByIdResponse>());
        }
        
    }
}
