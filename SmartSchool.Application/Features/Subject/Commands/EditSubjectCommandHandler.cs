using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Subject.Commands
{
    public class EditSubjectCommandHandler : ResponseHandler, IRequestHandler<EditSubjectCommand, Response<string>>
    {
        private readonly ISubjectRepsitory _subjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public EditSubjectCommandHandler(ISubjectRepsitory subjectRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetByIdAsync(request.Id);

            if (subject == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.SubjectNotFound]);

            // Check if name already exists for another subject
            var existingSubject = _subjectRepository.GetTableNoTracking()
                .FirstOrDefault(x => x.Name == request.Name && x.Id != request.Id);

            if (existingSubject != null)
                return UnprocessableEntity<string>(_stringLocalizer[SharedResourcesKeys.SubjectNameAlreadyExists]);

            subject.UpdateName(request.Name);
            subject.UpdatePeriod(request.Period);

            await _subjectRepository.UpdateAsync(subject);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.SubjectUpdatedSuccessfully]);
        }
    }
}