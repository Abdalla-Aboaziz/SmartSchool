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
    public class GetSubjectsListQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectByIdResponse>>>
    {
        private readonly ISubjectRepsitory _subjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetSubjectsListQueryHandler(ISubjectRepsitory subjectRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<List<GetSubjectByIdResponse>>> Handle(GetSubjectsListQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetListAsync();
            return Success(subjects.Adapt<List<GetSubjectByIdResponse>>());
        }
    }
}