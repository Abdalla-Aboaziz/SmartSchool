using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Instructor.Responses;
using SmartSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Instructor.Queries
{
    public class GetInstructorsListQuery : IRequest<Response<List<GetInstructorByIdResponse>>>
    {
    }
}