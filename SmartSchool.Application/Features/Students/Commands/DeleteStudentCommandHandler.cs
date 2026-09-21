using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Students.Commands
{
    public class DeleteStudentCommandHandler : ResponseHandler, IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            if (student is null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.StudentNotFound]);
            }

            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDeleteStudent]);
            }

            return Deleted<string>(_stringLocalizer[SharedResourcesKeys.StudentDeletedSuccessfully]);
        }
    }
}
