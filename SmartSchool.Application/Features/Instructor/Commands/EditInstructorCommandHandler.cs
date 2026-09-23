using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Instructor.Commands
{
    public class EditInstructorCommandHandler : ResponseHandler, IRequestHandler<EditInstructorCommand, Response<string>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentRepository _departmentRepository;

        public EditInstructorCommandHandler(IInstructorRepository instructorRepository,
            IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentRepository departmentRepository
            )
            : base(stringLocalizer)
        {
            _instructorRepository = instructorRepository;
            _stringLocalizer = stringLocalizer;
            _departmentRepository = departmentRepository;
        }
        public async Task<Response<string>> Handle(EditInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepository.GetByIdAsync(request.Id);

            if (instructor == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.InstructorNotFound]);

            // Check if name already exists for another instructor
            var existingInstructor = _instructorRepository.GetTableNoTracking()
                .FirstOrDefault(x => x.Name == request.Name && x.Id != request.Id);

            if (existingInstructor != null)
                return UnprocessableEntity<string>(_stringLocalizer[SharedResourcesKeys.InstructorNameAlreadyExists]);

            instructor.UpdateBasicInfo(
                request.Name,
                request.Address,
                request.Position
            );



            instructor.ChangeSalary(request.Salary);

            if (request.DepartmentId != instructor.DepartmentId)
            {
                var department = await _departmentRepository
                    .GetByIdAsync(request.DepartmentId.Value);

                if (department == null)
                    return NotFound<string>(
                      _stringLocalizer[SharedResourcesKeys.DepartmentNotFound]
                    );

                instructor.AssignToDepartment(department);
            }



            await _instructorRepository.UpdateAsync(instructor);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.InstructorUpdatedSuccessfully]);
        }
    }
}