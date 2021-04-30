using Microsoft.EntityFrameworkCore;

using static System.Console;

namespace CoursesAndStudents
{
  public class Academy : DbContext
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<Student> Courses { get; set; }

    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder)
    {
      string path = System.IO.Path.Combine(
        System.Environment.CurrentDirectory, "Academy.db");

      WriteLine($"Using {path} database file.");

      optionsBuilder.UseSqlite($"Filename={path}");

      // optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Academy;Integrated Security=true;MultipleActiveResultSets=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Fluent API validation rules

      modelBuilder.Entity<Student>()
        .Property(s => s.LastName).HasMaxLength(30).IsRequired();

      // populate database with sample data

      Student alice = new() { StudentId = 1, 
        FirstName = "Alice", LastName = "Jones" };
      Student bob = new() { StudentId = 2, 
        FirstName = "Bob", LastName = "Smith" };
      Student cecilia = new() { StudentId = 3, 
        FirstName = "Cecilia", LastName = "Ramirez" };

      Course csharp = new() 
      { 
        CourseId = 1,
        Title = "C# 10 and .NET 6", 
      };

      Course webdev = new()
      {
        CourseId = 2,
        Title = "Web Development",
      };

      Course python = new()
      {
        CourseId = 3,
        Title = "Python for Beginners",
      };

      modelBuilder.Entity<Student>()
        .HasData(alice, bob, cecilia);

      modelBuilder.Entity<Course>()
        .HasData(csharp, webdev, python);

      modelBuilder.Entity<Course>()
        .HasMany(c => c.Students)
        .WithMany(s => s.Courses)
        .UsingEntity(e => e.HasData(
          // all students signed up for C# course
          new { CoursesCourseId = 1, StudentsStudentId = 1 },
          new { CoursesCourseId = 1, StudentsStudentId = 2 },
          new { CoursesCourseId = 1, StudentsStudentId = 3 },
          // only Bob signed up for Web Dev
          new { CoursesCourseId = 2, StudentsStudentId = 2 },
          // only Cecilia signed up for Python
          new { CoursesCourseId = 3, StudentsStudentId = 3 }
        ));    }
  }
}