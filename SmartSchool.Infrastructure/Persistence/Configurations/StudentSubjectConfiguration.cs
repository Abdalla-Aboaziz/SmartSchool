using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class StudentSubjectConfiguration
    : IEntityTypeConfiguration<StudentSubject>
{
    public void Configure(EntityTypeBuilder<StudentSubject> builder)
    {
        builder.ToTable("StudentSubjects");

        builder.HasKey(x => new
        {
            x.StudentId,
            x.SubjectId
        });

        builder.HasOne(x => x.Student)
            .WithMany(x => x.StudentSubjects)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Subject)
            .WithMany(x => x.StudentsSubjects)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.Grade)
           .HasPrecision(5, 2)
           .IsRequired(false);


        // Prevent duplicate Student ↔ Subject relationship
        builder.HasIndex(x => new
        {
            x.StudentId,
            x.SubjectId
        })
        .IsUnique();
    }
}