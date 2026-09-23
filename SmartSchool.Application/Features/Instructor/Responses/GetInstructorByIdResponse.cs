using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Instructor.Responses
{
    public class GetInstructorByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? SupervisorId { get; set; }
        public string? SupervisorName { get; set; }
    }
}