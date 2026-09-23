using MediatR;
using SmartSchool.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Instructor.Commands
{
    public class AddInstructorCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
    }
}