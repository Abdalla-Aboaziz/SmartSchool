namespace SmartSchool.Domain.Entities;

public class InstructorSubject
{
    // EF Core
    private InstructorSubject()
    {
    }


    private InstructorSubject(
        Instructor instructor,
        Subject subject)
    {
        ArgumentNullException.ThrowIfNull(instructor);
        ArgumentNullException.ThrowIfNull(subject);

        Instructor = instructor;
        Subject = subject;

        InstructorId = instructor.Id;
        SubjectId = subject.Id;
    }


    public int InstructorId { get; private set; }

    public int SubjectId { get; private set; }


    public Instructor Instructor { get; private set; } = null!;

    public Subject Subject { get; private set; } = null!;


    public static InstructorSubject Create(
        Instructor instructor,
        Subject subject)
    {
        ArgumentNullException.ThrowIfNull(instructor);
        ArgumentNullException.ThrowIfNull(subject);

        return new InstructorSubject(
            instructor,
            subject);
    }
}