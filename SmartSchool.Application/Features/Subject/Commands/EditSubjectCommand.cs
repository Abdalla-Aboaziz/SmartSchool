using MediatR;
using SmartSchool.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Subject.Commands
{
    public class EditSubjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
    }
}