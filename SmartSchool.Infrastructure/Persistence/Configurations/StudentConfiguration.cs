using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Infrastructure.Persistence.configuration;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            builder.HasOne(x => x.Department).WithMany(x => x.Students).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.SetNull);
        }
    }
