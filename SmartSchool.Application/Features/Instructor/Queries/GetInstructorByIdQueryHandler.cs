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
    public class GetInstructorByIdQueryHandler : ResponseHandler, IRequestHandler<GetInstructorByIdQuery, Response<GetInstructorByIdResponse>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetInstructorByIdQueryHandler(IInstructorRepository instructorRepository, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _instructorRepository = instructorRepository;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<GetInstructorByIdResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId);

            if (instructor == null)
            {
                return NotFound<GetInstructorByIdResponse>(_stringLocalizer[SharedResourcesKeys.InstructorNotFound]);
            }
            return Success(instructor.Adapt<GetInstructorByIdResponse>());
        }

    }
}