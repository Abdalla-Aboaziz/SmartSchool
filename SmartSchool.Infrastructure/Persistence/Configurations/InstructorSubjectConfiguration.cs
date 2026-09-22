using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations
{
    public class InstructorSubjectConfiguration
     : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(
            EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x =>
                new
                {
                    x.InstructorId,
                    x.SubjectId
                });

            builder
                .HasOne(x => x.Instructor)
                .WithMany(x => x.InstructorSubjects)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Subject)
                .WithMany(x => x.InstructorSubjects)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
