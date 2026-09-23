namespace SmartSchool.Application.Features.Departments.Responses
{
    public class GetDepartmentsListResponse
    {
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int StudentCount { get; set; }

    }
}
