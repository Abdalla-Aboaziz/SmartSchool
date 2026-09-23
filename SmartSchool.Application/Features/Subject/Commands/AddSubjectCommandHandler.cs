using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Subject.Commands
{
    public class AddSubjectCommandHandler : ResponseHandler, IRequestHandler<AddSubjectCommand, Response<string>>
    {
        private readonly ISubjectRepsitory _subjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddSubjectCommandHandler(ISubjectRepsitory subjectRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            // Check if name already exists
            var existingSubject = _subjectRepository.GetTableNoTracking()
                .FirstOrDefault(x => x.Name == request.Name);

            if (existingSubject != null)
                return UnprocessableEntity<string>(_stringLocalizer[SharedResourcesKeys.SubjectNameAlreadyExists]);

            var subject = new SmartSchool.Domain.Entities.Subject(
                       request.Name,
                       request.Period
                   );

            await _subjectRepository.AddAsync(subject);

            return Created<string>(_stringLocalizer[SharedResourcesKeys.SubjectAddedSuccessfully]);
        }
    }
}