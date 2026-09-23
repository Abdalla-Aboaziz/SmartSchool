using MediatR;
using SmartSchool.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Subject.Commands
{
    public class DeleteSubjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}