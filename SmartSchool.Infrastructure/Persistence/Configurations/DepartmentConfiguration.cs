using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations
{
    //public class DepartmentConfiguration
    //: IEntityTypeConfiguration<Department>
    //{
    //    public void Configure(EntityTypeBuilder<Department> builder)
    //    {
    //        builder.ToTable("Departments");

    //        builder.HasKey(x => x.Id);

    //        builder.Property(x => x.Name)
    //            .IsRequired()
    //            .HasMaxLength(200);

    //        builder.HasMany(x => x.Students)
    //            .WithOne(x => x.Department)
    //            .HasForeignKey(x => x.DepartmentId);

    //        builder.HasMany(x => x.DepartmentSubjects)
    //            .WithOne(x => x.Department)
    //            .HasForeignKey(x => x.DepartmentId);
    //    }
    //}


    public class DepartmentConfiguration
    : IEntityTypeConfiguration<Department>
    {
        public void Configure(
            EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();


            // Department 1 : M Instructors

            builder
                .HasMany(x => x.Instructors)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            // Department 1 : 1 Manager

            builder
                .HasOne(x => x.Manager)
                .WithOne(x => x.ManagedDepartment)
                .HasForeignKey<Department>(x => x.ManagerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Navigation(x => x.Instructors)
                .UsePropertyAccessMode(
                    PropertyAccessMode.Field);

            builder.Navigation(x => x.DepartmentSubjects)
                .UsePropertyAccessMode(
                    PropertyAccessMode.Field);

            builder.Navigation(x => x.Students)
                .UsePropertyAccessMode(
                    PropertyAccessMode.Field);
        }
    }
}
