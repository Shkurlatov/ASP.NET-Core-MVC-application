using School.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School.Infrastructure.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>(ConfigureStudent);
            builder.Entity<Group>(ConfigureGroup);
            builder.Entity<Course>(ConfigureCourse);
        }

        private void ConfigureStudent(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("school_type_hilo")
               .IsRequired();

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

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("school_type_hilo")
               .IsRequired();

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

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("school_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cb => cb.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
