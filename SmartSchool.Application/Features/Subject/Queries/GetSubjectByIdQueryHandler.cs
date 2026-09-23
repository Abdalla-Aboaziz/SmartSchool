using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Subject.Responses;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Subject.Queries
{
    public class GetSubjectByIdQueryHandler : ResponseHandler, IRequestHandler<GetSubjectByIdQuery, Response<GetSubjectByIdResponse>>
    {
        private readonly ISubjectRepsitory _subjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetSubjectByIdQueryHandler(ISubjectRepsitory subjectRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<GetSubjectByIdResponse>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

            if (subject == null)
            {
                return NotFound<GetSubjectByIdResponse>(_stringLocalizer[SharedResourcesKeys.SubjectNotFound]); // TODO: Add this resource key
            }
            return Success(subject.Adapt<GetSubjectByIdResponse>());
        }

    }
}