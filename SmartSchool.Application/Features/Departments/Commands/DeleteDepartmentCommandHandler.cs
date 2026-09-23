using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommandHandler : ResponseHandler, IRequestHandler<DeleteDepartmentCommand, Response<string>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _departmentRepository = departmentRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);

            if (department is null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.DepartmentNotFound]);
            }

            var trans = _departmentRepository.BeginTransaction();
            try
            {
                await _departmentRepository.DeleteAsync(department);
                await trans.CommitAsync();
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDeleteDepartment]);
            }

            return Deleted<string>(_stringLocalizer[SharedResourcesKeys.DepartmentDeletedSuccessfully]);
        }
    }
}
