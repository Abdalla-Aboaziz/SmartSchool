using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Students.Commands
{
    public class AddStudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddStudentCommandHandler(IStudentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _studentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // Check if name already exists
            var existingStudent = _studentRepository.GetTableNoTracking()
                .FirstOrDefault(x => x.Name == request.Name);

            if (existingStudent != null)
                return UnprocessableEntity<string>(_stringLocalizer[SharedResourcesKeys.StudentNameAlreadyExists]);  //"Student Name Already Exist"

            var student = new Student(
                   request.Name,
                   request.Address,
                   request.Phone,
                   request.DepartmentId
               );

            await _studentRepository.AddAsync(student);

            return Created("Student Added Successfully");
        }
    }
}
