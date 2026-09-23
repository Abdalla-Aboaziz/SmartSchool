using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Subject.Responses;
using SmartSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Subject.Queries
{
    public class GetSubjectsListQuery : IRequest<Response<List<GetSubjectByIdResponse>>>
    {
    }
}