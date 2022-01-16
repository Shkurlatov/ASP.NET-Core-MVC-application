using School.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Infrastructure.Data
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

                schoolContext.Groups.AddRange(GetPreconfiguredGroups());
                await schoolContext.SaveChangesAsync();

                schoolContext.Students.AddRange(GetPreconfiguredStudents());
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

        private static IEnumerable<Group> GetPreconfiguredGroups()
        {
            return new List<Group>()
            {
                new Group
                {
                    CourseId = 1,
                    Name = "SR-01"
                },
                new Group
                {
                    CourseId = 2,
                    Name = "SR-02"
                },
                new Group
                {
                    CourseId = 1,
                    Name = "SR-03"
                },
                new Group
                {
                    CourseId = 3,
                    Name = "SR-04"
                },
                new Group
                {
                    CourseId = 4,
                    Name = "SR-05"
                },
                new Group
                {
                    CourseId = 2,
                    Name = "SR-06"
                },
                new Group
                {
                    CourseId = 1,
                    Name = "SR-07"
                },
                new Group
                {
                    CourseId = 5,
                    Name = "SR-08"
                }
            };
        }

        private static IEnumerable<Student> GetPreconfiguredStudents()
        {
            return new List<Student>()
            {
                new Student
                {
                    GroupId = 1,
                    FirstName = "Bryan", LastName = "Cranston"
                },
                new Student
                {
                    GroupId = 5,
                    FirstName = "Anna", LastName = "Gunn"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Aaron", LastName = "Paul"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Dean", LastName = "Norris"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Betsy", LastName = "Brandt"
                },
                new Student
                {
                    GroupId = 4,
                    FirstName = "RJ", LastName = "Mitte"
                },
                new Student
                {
                    GroupId = 7,
                    FirstName = "Giancarlo", LastName = "Esposito"
                },
                new Student
                {
                    GroupId = 3,
                    FirstName = "Bob", LastName = "Odenkirk"
                },
                new Student
                {
                    GroupId = 6,
                    FirstName = "Jonathan", LastName = "Banks"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Laura", LastName = "Fraser"
                },
                new Student
                {
                    GroupId = 4,
                    FirstName = "Jesse", LastName = "Plemons"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Steven", LastName = "Quezada"
                },
                new Student
                {
                    GroupId = 3,
                    FirstName = "Charles", LastName = "Baker"
                },
                new Student
                {
                    GroupId = 4,
                    FirstName = "Christopher", LastName = "Cousins"
                },
                new Student
                {
                    GroupId = 6,
                    FirstName = "Matt", LastName = "Jones"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Lavell", LastName = "Crawford"
                },
                new Student
                {
                    GroupId = 5,
                    FirstName = "Michael", LastName = "Wiles"
                },
                new Student
                {
                    GroupId = 8,
                    FirstName = "Ray", LastName = "Campbell"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Emily", LastName = "Rios"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Carmen", LastName = "Serano"
                },
                new Student
                {
                    GroupId = 4,
                    FirstName = "Krysten", LastName = "Ritter"
                },
                new Student
                {
                    GroupId = 7,
                    FirstName = "Mark", LastName = "Margolis"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "David", LastName = "Costabile"
                },
                new Student
                {
                    GroupId = 3,
                    FirstName = "Michael", LastName = "Bowen"
                },
                new Student
                {
                    GroupId = 6,
                    FirstName = "Kevin", LastName = "Rankin"
                },
                new Student
                {
                    GroupId = 4,
                    FirstName = "Daniel", LastName = "Moncada"
                },
                new Student
                {
                    GroupId = 5,
                    FirstName = "Jessica", LastName = "Hecht"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Luis", LastName = "Moncada"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Bill", LastName = "Burr"
                },
                new Student
                {
                    GroupId = 7,
                    FirstName = "Tom", LastName = "Kiesche"
                },
                new Student
                {
                    GroupId = 4,
                    FirstName = "Tess", LastName = "Harper"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "John", LastName = "de Lancie"
                },
                new Student
                {
                    GroupId = 3,
                    FirstName = "Maurice", LastName = "Compte"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Jere", LastName = "Burns"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Nigel", LastName = "Gibbs"
                },
                new Student
                {
                    GroupId = 7,
                    FirstName = "Raymond", LastName = "Cruz"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Michael", LastName = "Bofshever"
                },
                new Student
                {
                    GroupId = 5,
                    FirstName = "Louis", LastName = "Ferreira"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Mike", LastName = "Batayeh"
                },
                new Student
                {
                    GroupId = 8,
                    FirstName = "Gonzalo", LastName = "Menendez"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Adam", LastName = "Godley"
                },
                new Student
                {
                    GroupId = 6,
                    FirstName = "BrJavieryan", LastName = "Grajeda"
                },
                new Student
                {
                    GroupId = 2,
                    FirstName = "Max", LastName = "Arciniega"
                },
                new Student
                {
                    GroupId = 1,
                    FirstName = "Julie", LastName = "Dretzin"
                },
                new Student
                {
                    GroupId = 3,
                    FirstName = "Julia", LastName = "Minesci"
                }
            };
        }
    }
}
