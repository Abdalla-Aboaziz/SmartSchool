using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Instructor.Responses;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Instructor.Queries
{
    public class GetInstructorsListQueryHandler : ResponseHandler, IRequestHandler<GetInstructorsListQuery, Response<List<GetInstructorByIdResponse>>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetInstructorsListQueryHandler(IInstructorRepository instructorRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _instructorRepository = instructorRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<List<GetInstructorByIdResponse>>> Handle(GetInstructorsListQuery request, CancellationToken cancellationToken)
        {
            var instructors = await _instructorRepository.GetListAsync();
            return Success(instructors.Adapt<List<GetInstructorByIdResponse>>());
        }
    }
}