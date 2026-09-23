using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Application.Features.Subject.Responses
{
    public class GetSubjectByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public int StudentCount { get; set; }
        public int DepartmentCount { get; set; }
        public int InstructorCount { get; set; }
    }
}