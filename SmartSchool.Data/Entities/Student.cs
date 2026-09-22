
namespace SmartSchool.Domain.Entities;

public class Student
{
    private readonly List<StudentSubject> _studentSubjects = [];
    // EF Core
    private Student()
    {
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public string Phone { get; private set; } = null!;

    public int? DepartmentId { get; private set; }


    public Department? Department { get; private set; }

    public IReadOnlyCollection<StudentSubject> StudentSubjects
        => _studentSubjects;


    // Domain Constructor
    public Student(
        string name,
        string address,
        string phone,
        int? departmentId = null)
    {
        SetName(name);
        SetAddress(address);
        SetPhone(phone);

        DepartmentId = departmentId;
    }


    // =========================
    // Domain Behavior
    // =========================

    public void UpdateName(string name)
    {
        SetName(name);
    }

    public void UpdateAddress(string address)
    {
        SetAddress(address);
    }

    public void UpdatePhone(string phone)
    {
        SetPhone(phone);
    }

    public void AssignToDepartment(int departmentId)
    {
        if (departmentId <= 0)
            throw new ArgumentException(
                "Department ID must be greater than zero.",
                nameof(departmentId));

        DepartmentId = departmentId;
    }

    public void RemoveFromDepartment()
    {
        DepartmentId = null;
        Department = null;
    }


    // =========================
    // Domain Rules
    // =========================

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Student name is required.",
                nameof(name));

        name = name.Trim();

        if (name.Length > 200)
            throw new ArgumentException(
                "Student name cannot exceed 200 characters.",
                nameof(name));

        Name = name;
    }

    private void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Student address is required.",
                nameof(address));

        address = address.Trim();

        if (address.Length > 200)
            throw new ArgumentException(
                "Student address cannot exceed 200 characters.",
                nameof(address));

        Address = address;
    }

    private void SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException(
                "Student phone is required.",
                nameof(phone));

        Phone = phone.Trim();
    }
}

