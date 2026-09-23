namespace SmartSchool.Domain.Entities;

public class StudentSubject
{
    // EF Core
    private StudentSubject()
    {
    }

    public StudentSubject(
        int studentId,
        int subjectId)
    {
        if (studentId <= 0)
            throw new ArgumentException(
                "Student ID must be greater than zero.",
                nameof(studentId));

        if (subjectId <= 0)
            throw new ArgumentException(
                "Subject ID must be greater than zero.",
                nameof(subjectId));

        StudentId = studentId;
        SubjectId = subjectId;
    }

    public int StudentId { get; private set; }

    public int SubjectId { get; private set; }

    public decimal? Grade { get; private set; }

    // Navigation Properties

    public Student Student { get; private set; } = null!;

    public Subject Subject { get; private set; } = null!;


    // Domain Behavior

    public void SetGrade(decimal grade)
    {
        if (grade < 0 || grade > 100)
            throw new ArgumentOutOfRangeException(
                nameof(grade),
                "Grade must be between 0 and 100.");

        Grade = grade;
    }

    public bool HasGrade()
    {
        return Grade.HasValue;
    }

    public bool IsPassed()
    {
        return Grade.HasValue && Grade.Value >= 50;
    }
}