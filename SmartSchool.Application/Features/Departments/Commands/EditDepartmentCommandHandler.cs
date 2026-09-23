using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class EditDepartmentCommandHandler
        : ResponseHandler,
          IRequestHandler<EditDepartmentCommand, Response<string>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public EditDepartmentCommandHandler(IDepartmentRepository departmentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _departmentRepository = departmentRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(
         EditDepartmentCommand request,
         CancellationToken cancellationToken)
        {
            var existingDepartment =
                await _departmentRepository.GetByIdAsync(request.Id);

            if (existingDepartment is null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.DepartmentNotFound]);
            }

            if (request.Name is not null)
            {
                existingDepartment.UpdateName(request.Name);
            }


            await _departmentRepository.UpdateAsync(existingDepartment);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.DepartmentUpdatedSuccessfully]);
        }
    }}