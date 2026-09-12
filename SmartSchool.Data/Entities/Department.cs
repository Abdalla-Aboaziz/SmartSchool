
namespace SmartSchool.Domain.Entities;

public class Department
{
    private readonly List<Student> _students = [];
    private readonly List<DepartmentSubject> _departmentSubjects = [];

    // EF Core
    private Department()
    {
    }

    public Department(string name)
    {
        SetName(name);
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    // Read-only collections
    public IReadOnlyCollection<Student> Students => _students;

    public IReadOnlyCollection<DepartmentSubject> DepartmentSubjects
        => _departmentSubjects;


    // =========================
    // Domain Behavior
    // =========================

    public void UpdateName(string name)
    {
        SetName(name);
    }


    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Contains(student))
            return;

        _students.Add(student);
    }


    public void RemoveStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        _students.Remove(student);
    }


    public void AddSubject(DepartmentSubject departmentSubject)
    {
        ArgumentNullException.ThrowIfNull(departmentSubject);

        if (_departmentSubjects.Contains(departmentSubject))
            return;

        _departmentSubjects.Add(departmentSubject);
    }


    public void RemoveSubject(DepartmentSubject departmentSubject)
    {
        ArgumentNullException.ThrowIfNull(departmentSubject);

        _departmentSubjects.Remove(departmentSubject);
    }


    // =========================
    // Domain Rules
    // =========================

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Department name is required.",
                nameof(name));

        name = name.Trim();

        if (name.Length > 200)
            throw new ArgumentException(
                "Department name cannot exceed 200 characters.",
                nameof(name));

        Name = name;
    }
}

