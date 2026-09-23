using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Subject.Commands
{
    public class DeleteSubjectCommandHandler : ResponseHandler, IRequestHandler<DeleteSubjectCommand, Response<string>>
    {
        private readonly ISubjectRepsitory _subjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DeleteSubjectCommandHandler(ISubjectRepsitory subjectRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetByIdAsync(request.Id);

            if (subject == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.SubjectNotFound]);

            await _subjectRepository.DeleteAsync(subject);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.SubjectDeletedSuccessfully]);
        }
    }
}