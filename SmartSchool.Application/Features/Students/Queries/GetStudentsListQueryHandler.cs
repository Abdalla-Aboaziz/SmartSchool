using Mapster;
using MediatR;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;
namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentsListQueryHandler : ResponseHandler, IRequestHandler<GetStudentsListQuery, Response<List<GetStudentsListResponse>>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentsListQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Response<List<GetStudentsListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {

            var studentList = await _studentRepository.GetStudentsListAsync();
            return Success(studentList.Adapt<List<GetStudentsListResponse>>());
        }

    }

}

