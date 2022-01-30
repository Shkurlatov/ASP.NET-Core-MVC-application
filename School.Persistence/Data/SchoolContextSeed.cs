using School.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Persistence.Data
{
    public class SchoolContextSeed
    {
        public static async Task SeedAsync(SchoolContext schoolContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                schoolContext.Database.EnsureCreated();

                if (schoolContext.Courses.Any())
                {
                    return;
                }

                schoolContext.Courses.AddRange(GetPreconfiguredCourses());
                await schoolContext.SaveChangesAsync();

                schoolContext.Groups.AddRange(GetPreconfiguredGroups(schoolContext.Courses));
                await schoolContext.SaveChangesAsync();

                schoolContext.Students.AddRange(GetPreconfiguredStudents(schoolContext.Groups));
                await schoolContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<SchoolContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(schoolContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Course> GetPreconfiguredCourses()
        {
            return new List<Course>()
            {
                new Course
                {
                    Name = "Java Spring",
                    Description = "You can find work in almost any industry, from game development to the space industry"
                },
                new Course
                {
                    Name = "C#/.Net",
                    Description = "Create different types of applications, from websites to computer games and mobile platforms"
                },
                new Course
                {
                    Name = "Automation QA",
                    Description = "Learn to create scripts and automate software testing processes"
                },
                new Course
                {
                    Name = "Phyton",
                    Description = "Master the basics of Python to build apps and websites"
                },
                new Course
                {
                    Name = "Game Dev",
                    Description = "Creating games from a programming point of view, basic principles and features of the industry"
                }
            };
        }

        private static IEnumerable<Group> GetPreconfiguredGroups(IEnumerable<Course> courses)
        {
            return new List<Group>()
            {
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "Java Spring").Id,
                    Name = "SR-01"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "C#/.Net").Id,
                    Name = "SR-02"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "Java Spring").Id,
                    Name = "SR-03"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "Automation QA").Id,
                    Name = "SR-04"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "Phyton").Id,
                    Name = "SR-05"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "C#/.Net").Id,
                    Name = "SR-06"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "Java Spring").Id,
                    Name = "SR-07"
                },
                new Group
                {
                    CourseId = courses.Single( c => c.Name == "Game Dev").Id,
                    Name = "SR-08"
                }
            };
        }

        private static IEnumerable<Student> GetPreconfiguredStudents(IEnumerable<Group> groups)
        {
            return new List<Student>()
            {
                 new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Bryan", LastName = "Cranston"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-05").Id,
                    FirstName = "Anna", LastName = "Gunn"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Aaron", LastName = "Paul"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Dean", LastName = "Norris"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Betsy", LastName = "Brandt"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-04").Id,
                    FirstName = "RJ", LastName = "Mitte"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-07").Id,
                    FirstName = "Giancarlo", LastName = "Esposito"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-03").Id,
                    FirstName = "Bob", LastName = "Odenkirk"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-06").Id,
                    FirstName = "Jonathan", LastName = "Banks"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Laura", LastName = "Fraser"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-04").Id,
                    FirstName = "Jesse", LastName = "Plemons"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Steven", LastName = "Quezada"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-03").Id,
                    FirstName = "Charles", LastName = "Baker"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-04").Id,
                    FirstName = "Christopher", LastName = "Cousins"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-06").Id,
                    FirstName = "Matt", LastName = "Jones"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Lavell", LastName = "Crawford"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-05").Id,
                    FirstName = "Michael", LastName = "Wiles"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-08").Id,
                    FirstName = "Ray", LastName = "Campbell"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Emily", LastName = "Rios"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Carmen", LastName = "Serano"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-04").Id,
                    FirstName = "Krysten", LastName = "Ritter"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-07").Id,
                    FirstName = "Mark", LastName = "Margolis"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "David", LastName = "Costabile"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-03").Id,
                    FirstName = "Michael", LastName = "Bowen"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-06").Id,
                    FirstName = "Kevin", LastName = "Rankin"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-04").Id,
                    FirstName = "Daniel", LastName = "Moncada"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-05").Id,
                    FirstName = "Jessica", LastName = "Hecht"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Luis", LastName = "Moncada"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Bill", LastName = "Burr"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-07").Id,
                    FirstName = "Tom", LastName = "Kiesche"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-04").Id,
                    FirstName = "Tess", LastName = "Harper"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "John", LastName = "de Lancie"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-03").Id,
                    FirstName = "Maurice", LastName = "Compte"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Jere", LastName = "Burns"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Nigel", LastName = "Gibbs"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-07").Id,
                    FirstName = "Raymond", LastName = "Cruz"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Michael", LastName = "Bofshever"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-05").Id,
                    FirstName = "Louis", LastName = "Ferreira"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Mike", LastName = "Batayeh"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-08").Id,
                    FirstName = "Gonzalo", LastName = "Menendez"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Adam", LastName = "Godley"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-06").Id,
                    FirstName = "BrJavieryan", LastName = "Grajeda"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-02").Id,
                    FirstName = "Max", LastName = "Arciniega"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-01").Id,
                    FirstName = "Julie", LastName = "Dretzin"
                },
                new Student
                {
                    GroupId = groups.Single( g => g.Name == "SR-03").Id,
                    FirstName = "Julia", LastName = "Minesci"
                }
            };
        }
    }
}
