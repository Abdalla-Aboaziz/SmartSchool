using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

    public class DepartmentSubjectConfiguration
    : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(
            EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.ToTable("DepartmentSubjects");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.DepartmentSubjects)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Subject)
                .WithMany(x => x.DepartmentSubjects)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent duplicate relationship
            builder.HasIndex(x => new
            {
                x.DepartmentId,
                x.SubjectId
            })
            .IsUnique();
        }
    }