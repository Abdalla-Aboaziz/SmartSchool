using MediatR;
using SmartSchool.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Instructor.Commands
{
    public class DeleteInstructorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}