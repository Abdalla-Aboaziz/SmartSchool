using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Students.Responses
{
    public class GetStudentByIdResponse
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? DepartmentName { get; set; }

    }
}
