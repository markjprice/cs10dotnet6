using Microsoft.EntityFrameworkCore; // for GenerateCreateScript()
using System.Threading.Tasks; // Task
using static System.Console;

namespace CoursesAndStudents
{
  class Program
  {
    async static Task Main(string[] args)
    {
      using (Academy a = new())
      {
        bool deleted = await a.Database.EnsureDeletedAsync();
        WriteLine($"Database deleted: {deleted}");

        bool created = await a.Database.EnsureCreatedAsync();
        WriteLine($"Database created: {created}");

        WriteLine("SQL script used to create database:");
        WriteLine(a.Database.GenerateCreateScript());

        foreach (Student s in a.Students.Include(s => s.Courses))
        {
          WriteLine("{0} {1} attends the following courses:",
            s.FirstName, s.LastName, s.Courses.Count);

          foreach (Course c in s.Courses)
          {
            WriteLine($"  {c.Title});
          }
        }
      }
    }
  }
}