using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class AddDepartmentCommandHandler : ResponseHandler, IRequestHandler<AddDeparmentCommand, Response<string>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddDepartmentCommandHandler(IDepartmentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _departmentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(AddDeparmentCommand request, CancellationToken cancellationToken)
        {
            // Check if name already exists
            var existingDepartment = _departmentRepository.GetTableNoTracking()
                .FirstOrDefault(x => x.Name == request.Name);

            if (existingDepartment != null)
                return UnprocessableEntity<string>(_stringLocalizer[SharedResourcesKeys.DepartmentNameAlreadyExists]);  //"Department Name Already Exist"

            var department = new Department(
                request.Name
            );

            await _departmentRepository.AddAsync(department);

            return Created<string>(_stringLocalizer[SharedResourcesKeys.DepartmentAddedSuccessfully]);
        }
    }
}
