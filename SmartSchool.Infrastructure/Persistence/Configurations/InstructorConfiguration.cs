using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations
{
    public class InstructorConfiguration
      : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(
            EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(x => x.Address)
                .HasMaxLength(300)
                .IsRequired();

            builder
                .Property(x => x.Position)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Salary)
                .HasPrecision(18, 2);


            // Instructor -> Department

            builder
                .HasOne(x => x.Department)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.DepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            // Instructor -> Supervisor (Self Reference)

            builder
                .HasOne(x => x.Supervisor)
                .WithMany(x => x.Subordinates)
                .HasForeignKey(x => x.SupervisorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            // Instructor -> Managed Department
            // The inverse side is already configured
            // by DepartmentConfiguration.

            builder.Navigation(x => x.Subordinates)
                .UsePropertyAccessMode(
                    PropertyAccessMode.Field);

            builder.Navigation(x => x.InstructorSubjects)
                .UsePropertyAccessMode(
                    PropertyAccessMode.Field);
        }
    }
}
