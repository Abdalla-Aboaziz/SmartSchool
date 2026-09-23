using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Instructor.Commands
{
    public class DeleteInstructorCommandHandler : ResponseHandler, IRequestHandler<DeleteInstructorCommand, Response<string>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DeleteInstructorCommandHandler(IInstructorRepository instructorRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _instructorRepository = instructorRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepository.GetByIdAsync(request.Id);

            if (instructor == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.InstructorNotFound]);

            await _instructorRepository.DeleteAsync(instructor);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.InstructorDeletedSuccessfully]);
        }
    }
}