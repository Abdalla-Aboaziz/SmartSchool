namespace SmartSchool.Domain.Entities;

public class Instructor
{
    private readonly List<Instructor> _subordinates = [];
    private readonly List<InstructorSubject> _instructorSubjects = [];


    // EF Core
    private Instructor()
    {
    }


    public Instructor(
        string name,
        string address,
        string position,
        decimal salary)
    {
        SetName(name);
        SetAddress(address);
        SetPosition(position);
        SetSalary(salary);
    }


    // =========================
    // Properties
    // =========================

    public int Id { get; private set; }

    public string? Name { get; private set; }

    public string? Address { get; private set; }

    public string? Position { get; private set; }

    public decimal Salary { get; private set; }


    // Department
    public int? DepartmentId { get; private set; }

    public Department? Department { get; private set; }


    // Supervisor
    public int? SupervisorId { get; private set; }

    public Instructor? Supervisor { get; private set; }


    // Department Manager
    public Department? ManagedDepartment { get; private set; }


    // =========================
    // Read-only Collections
    // =========================

    public IReadOnlyCollection<Instructor> Subordinates
        => _subordinates;

    public IReadOnlyCollection<InstructorSubject> InstructorSubjects
        => _instructorSubjects;


    // =========================
    // Domain Behavior
    // =========================

    public void UpdateBasicInfo(
        string name,
        string address,
        string position)
    {
        SetName(name);
        SetAddress(address);
        SetPosition(position);
    }


    public void ChangeSalary(decimal salary)
    {
        SetSalary(salary);
    }


    // -------------------------
    // Department
    // -------------------------

    internal void AssignToDepartment(Department department)
    {
        ArgumentNullException.ThrowIfNull(department);

        Department = department;

        if (department.Id > 0)
            DepartmentId = department.Id;
    }

    internal void RemoveFromDepartment()
    {
        Department = null;
        DepartmentId = null;
    }


    // -------------------------
    // Supervisor
    // -------------------------

    public void AssignSupervisor(Instructor supervisor)
    {
        ArgumentNullException.ThrowIfNull(supervisor);

        if (ReferenceEquals(this, supervisor))
        {
            throw new InvalidOperationException(
                "An instructor cannot be their own supervisor.");
        }

        Supervisor = supervisor;

        if (supervisor.Id > 0)
            SupervisorId = supervisor.Id;

        if (!supervisor._subordinates.Contains(this))
        {
            supervisor._subordinates.Add(this);
        }
    }


    public void RemoveSupervisor()
    {
        if (Supervisor is not null)
        {
            Supervisor._subordinates.Remove(this);
        }

        Supervisor = null;
        SupervisorId = null;
    }


    // -------------------------
    // Subjects
    // -------------------------

    public void AssignSubject(Subject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        if (_instructorSubjects.Any(x => x.SubjectId == subject.Id))
            return;

        var instructorSubject =
            InstructorSubject.Create(this, subject);

        _instructorSubjects.Add(instructorSubject);
    }


    public void RemoveSubject(Subject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        var instructorSubject = _instructorSubjects
            .FirstOrDefault(x => x.SubjectId == subject.Id);

        if (instructorSubject is null)
            return;

        _instructorSubjects.Remove(instructorSubject);
    }


    // =========================
    // Domain Rules
    // =========================

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Instructor name is required.",
                nameof(name));
        }

        name = name.Trim();

        if (name.Length > 200)
        {
            throw new ArgumentException(
                "Instructor name cannot exceed 200 characters.",
                nameof(name));
        }

        Name = name;
    }


    private void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException(
                "Instructor address is required.",
                nameof(address));
        }

        address = address.Trim();

        if (address.Length > 300)
        {
            throw new ArgumentException(
                "Instructor address cannot exceed 300 characters.",
                nameof(address));
        }

        Address = address;
    }


    private void SetPosition(string position)
    {
        if (string.IsNullOrWhiteSpace(position))
        {
            throw new ArgumentException(
                "Instructor position is required.",
                nameof(position));
        }

        position = position.Trim();

        if (position.Length > 100)
        {
            throw new ArgumentException(
                "Instructor position cannot exceed 100 characters.",
                nameof(position));
        }

        Position = position;
    }


    private void SetSalary(decimal salary)
    {
        if (salary < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(salary),
                "Salary cannot be negative.");
        }

        Salary = salary;
    }
}