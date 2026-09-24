namespace SmartSchool.Domain.Entities;

public class Department : AuditableEntity
{
    private readonly List<Student> _students = [];
    private readonly List<DepartmentSubject> _departmentSubjects = [];
    private readonly List<Instructor> _instructors = [];

    // EF Core
    private Department()
    {
    }

    public Department(string name)
    {
        SetName(name);
    }

    public int Id { get; private set; }

    public string? Name { get; private set; }

    // Department Manager
    public int? ManagerId { get; private set; }

    public Instructor? Manager { get; private set; }


    // =========================
    // Read-only Collections
    // =========================

    public IReadOnlyCollection<Student> Students
        => _students;

    public IReadOnlyCollection<DepartmentSubject> DepartmentSubjects
        => _departmentSubjects;

    public IReadOnlyCollection<Instructor> Instructors
        => _instructors;


    // =========================
    // Domain Behavior
    // =========================

    public void UpdateName(string name)
    {
        SetName(name);
    }


    // -------------------------
    // Students
    // -------------------------

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


    // -------------------------
    // Subjects
    // -------------------------

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


    // -------------------------
    // Instructors
    // -------------------------

    public void AddInstructor(Instructor instructor)
    {
        ArgumentNullException.ThrowIfNull(instructor);

        if (_instructors.Contains(instructor))
            return;

        _instructors.Add(instructor);

        instructor.AssignToDepartment(this);
    }

    public void RemoveInstructor(Instructor instructor)
    {
        ArgumentNullException.ThrowIfNull(instructor);

        if (ReferenceEquals(Manager, instructor))
        {
            throw new InvalidOperationException(
                "The department manager must be removed before removing the instructor.");
        }

        _instructors.Remove(instructor);

        instructor.RemoveFromDepartment();
    }


    // -------------------------
    // Manager
    // -------------------------

    public void AssignManager(Instructor instructor)
    {
        ArgumentNullException.ThrowIfNull(instructor);

        if (!_instructors.Contains(instructor))
        {
            throw new InvalidOperationException(
                "The manager must belong to the department.");
        }

        Manager = instructor;

        // When the entity is already persisted, keep FK in sync.
        if (instructor.Id > 0)
            ManagerId = instructor.Id;
    }

    public void RemoveManager()
    {
        Manager = null;
        ManagerId = null;
    }


    // =========================
    // Domain Rules
    // =========================

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Department name is required.",
                nameof(name));
        }

        name = name.Trim();

        if (name.Length > 200)
        {
            throw new ArgumentException(
                "Department name cannot exceed 200 characters.",
                nameof(name));
        }

        Name = name;
    }
}