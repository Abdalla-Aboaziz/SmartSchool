using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Instructor.Commands
{
    public class AddInstructorCommandHandler : ResponseHandler, IRequestHandler<AddInstructorCommand, Response<string>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentRepository _departmentRepository;

        public AddInstructorCommandHandler(IInstructorRepository instructorRepository,
            IStringLocalizer<SharedResources> stringLocalizer,
              IDepartmentRepository departmentRepository
            )
            : base(stringLocalizer)
        {
            _instructorRepository = instructorRepository;
            _stringLocalizer = stringLocalizer;
            _departmentRepository = departmentRepository;
        }
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            // Check if name already exists
            var existingInstructor = await _instructorRepository
                      .GetTableNoTracking()
                      .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (existingInstructor != null)
                return UnprocessableEntity<string>(_stringLocalizer[SharedResourcesKeys.InstructorNameAlreadyExists]); // TODO: Add this resource key

            var instructor = new SmartSchool.Domain.Entities.Instructor(
                       request.Name,
                       request.Address,
                       request.Position,
                       request.Salary
                   );

            // Handle optional relationships
            if (request.DepartmentId.HasValue)
            {
                var department =
                    await _departmentRepository.GetByIdAsync(request.DepartmentId.Value);

                if (department == null)
                    return NotFound<string>(
                        _stringLocalizer[SharedResourcesKeys.DepartmentNotFound]);

                instructor.AssignToDepartment(department);
            }


            if (request.SupervisorId.HasValue)
            {
                var supervisor =
                    await _instructorRepository.GetByIdAsync(request.SupervisorId.Value);

                if (supervisor == null)
                    return NotFound<string>(
                        _stringLocalizer[SharedResourcesKeys.InstructorNotFound]);

                instructor.AssignSupervisor(supervisor);
            }

            await _instructorRepository.AddAsync(instructor);

            return Created<string>(_stringLocalizer[SharedResourcesKeys.InstructorAddedSuccessfully]);
        }
    }
}