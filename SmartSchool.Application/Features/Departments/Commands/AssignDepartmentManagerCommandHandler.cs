using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class AssignDepartmentManagerCommandHandler : ResponseHandler, IRequestHandler<AssignDepartmentManagerCommand, Response<string>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IInstructorRepository _instructorRepository;

        public AssignDepartmentManagerCommandHandler(IDepartmentRepository studentRepository,
            IStringLocalizer<SharedResources> stringLocalizer,
            IInstructorRepository instructorRepository
            )
            : base(stringLocalizer)
        {
            _departmentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
            _instructorRepository = instructorRepository;
        }
        public async Task<Response<string>> Handle(AssignDepartmentManagerCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentsQueryable().Include(i => i.Instructors)
                .FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);

            if (department != null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.DepartmentNotFound]);

            var instructor = await _instructorRepository.GetTableAsTracking().Where(i => i.Id == request.InstructorId).FirstOrDefaultAsync(cancellationToken);
            if (instructor == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.InstructorNotFound]);

            if (!department.Instructors.Any(i => i.Id == instructor.Id))
                return UnprocessableEntity<string>(
                    _stringLocalizer["The manager must belong to the department."]);
            department.AssignManager(instructor);
            await _departmentRepository.UpdateAsync(department);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}
