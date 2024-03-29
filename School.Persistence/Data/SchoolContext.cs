﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities.Studies;
using School.Domain.Entities.Users;

namespace School.Persistence.Data
{
    public class SchoolContext : IdentityDbContext<User>
    {
        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Curator> Curators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>(ConfigureStudent);
            builder.Entity<Group>(ConfigureGroup);
            builder.Entity<Course>(ConfigureCourse);
            base.OnModelCreating(builder);
        }

        private void ConfigureStudent(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.GroupId)
                .IsRequired();

            builder.Property(cb => cb.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cb => cb.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }

        private void ConfigureGroup(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.CourseId)
                .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(50);
        }

        private void ConfigureCourse(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cb => cb.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
