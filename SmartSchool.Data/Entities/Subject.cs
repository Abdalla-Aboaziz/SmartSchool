
namespace SmartSchool.Domain.Entities;

public class Subject : AuditableEntity
{
    private readonly List<StudentSubject> _studentsSubjects = [];
    private readonly List<DepartmentSubject> _departmentSubjects = [];
    private readonly List<InstructorSubject> _instructorSubjects = [];



    // EF Core
    private Subject()
    {
    }

    public Subject(
        string name,
        int period)
    {
        SetName(name);
        SetPeriod(period);
    }

    public int Id { get; private set; }

    public string? Name { get; private set; }

    public int? Period { get; private set; }


    // =========================
    // Navigation Collections
    // =========================

    public IReadOnlyCollection<StudentSubject> StudentsSubjects
        => _studentsSubjects;

    public IReadOnlyCollection<DepartmentSubject> DepartmentSubjects
        => _departmentSubjects;
    public IReadOnlyCollection<InstructorSubject> InstructorSubjects
       => _instructorSubjects;

    // =========================
    // Domain Behavior
    // =========================

    public void UpdateName(string name)
    {
        SetName(name);
    }

    public void UpdatePeriod(int period)
    {
        SetPeriod(period);
    }


    // =========================
    // Domain Rules
    // =========================

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Subject name is required.",
                nameof(name));

        name = name.Trim();

        if (name.Length > 200)
            throw new ArgumentException(
                "Subject name cannot exceed 200 characters.",
                nameof(name));

        Name = name;
    }

    private void SetPeriod(int period)
    {
        if (period <= 0)
            throw new ArgumentException(
                "Subject period is required.",
                nameof(period));

        Period = period;
    }
}

