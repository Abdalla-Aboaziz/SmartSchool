
namespace SmartSchool.Domain.Entities;

public class DepartmentSubject
{
    // EF Core
    private DepartmentSubject()
    {
    }

    public DepartmentSubject(
        int departmentId,
        int subjectId)
    {
        if (departmentId <= 0)
            throw new ArgumentException(
                "Department ID must be greater than zero.",
                nameof(departmentId));

        if (subjectId <= 0)
            throw new ArgumentException(
                "Subject ID must be greater than zero.",
                nameof(subjectId));

        DepartmentId = departmentId;
        SubjectId = subjectId;
    }

    public int Id { get; private set; }

    public int DepartmentId { get; private set; }

    public int SubjectId { get; private set; }


    // Navigation Properties

    public Department Department { get; private set; } = null!;

    public Subject Subject { get; private set; } = null!;
}

