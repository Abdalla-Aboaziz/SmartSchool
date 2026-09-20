using MediatR;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Responses;
using SmartSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<Response<GetStudentByIdResponse>>
    {
        public GetStudentByIdQuery(int studentId)
        {
            StudentId = studentId;
        }
        public int StudentId { get; set; }
    }
}
