﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_school.Models
{
    public static class UserSeed
    {
        public static async Task SeedDataAsync(SchoolDb db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedClassesAsync(db);
            await SeedUsersAsync(userManager);
            await SeedSubjectsAsync(db);
            await SeedGradesAsync(db);          
            await SeedTeacherSubjectAsync(db, userManager);
            await SeedClassSubjectsAsync(db);
        }
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (await userManager.FindByEmailAsync("a@a.hu") == null)
            {
                User user = new User()
                {
                    Email = "a@a.hu",
                    BirthDate = new DateOnly(1994, 12, 16),
                    PasswordHash = "5c222a477001ef63ec220dd86c27f952a55397b4987749bed0bb2568a82292ac",
                    UserName = "a@a.hu",
                    PhoneNumber = "0036309833728",
                    PhoneNumberConfirmed = true,
                    ClassId = 1
                };

                IdentityResult result = await userManager.CreateAsync(user);
                Console.WriteLine(result);
            }
            if (await userManager.FindByEmailAsync("d@d.hu") == null)
            {
                    User user1 = new User()
                    {
                        Email = "d@d.hu",
                        BirthDate = new DateOnly(1994, 12, 16),
                        PasswordHash = "5c222a477001ef63ec220dd86c27f952a55397b4987749bed0bb2568a82292ac",
                        UserName = "d@d.hu",
                        PhoneNumber = "0036309833728",
                        PhoneNumberConfirmed = true,
                        ClassId = 1
                    };

                    IdentityResult result1 = await userManager.CreateAsync(user1);
                    Console.WriteLine(result1);
            }
        }
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = ["Student", "Teacher", "Admin", "NormalUser", "Unclassified"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                else
                {
                    Console.WriteLine($"{role} szerepkör már létezik"); ;
                }
            }
        }



        public static async Task SeedGradesAsync(SchoolDb db)
        {
            User student = await db.Users.FirstOrDefaultAsync(x => x.Email == "a@a.hu");
            User teacher = await db.Users.FirstOrDefaultAsync(x => x.Email == "b@b.hu");
            Subject subject = await db.Subjects.FirstOrDefaultAsync(x => x.Name == "Informatika");

            Grade grade = new Grade()
            {
                Date = DateTime.Now,
                StudentId = student.Id,
                Student = student,
                Value = 1,
                TeacherId = teacher.Id,
                Teacher = teacher,
                SubjectId = subject.Id,
                Subject = subject
            };

            db.Grades.Update(grade);
            await db.SaveChangesAsync();

            var query = db.Grades.Include(x => x.Student).Where(x => x.Student.Email == "a@a.hu").Where(y => y.Value == 1);
            //var student1 = query.

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Value}: {item.Student.Email}, {item.Teacher.Email}");
            }
        }

        public static async Task SeedSubjectsAsync(SchoolDb db)
        {
            var subject = new Subject()
            {
                Name = "Informatika",
            };

            if (await db.Subjects.FirstOrDefaultAsync(x => x.Name == subject.Name) == null)
            {
                db.Subjects.Update(subject);
                await db.SaveChangesAsync();
            }

            else
            {
                Console.WriteLine("Már létezik");
            }

        }

        public static async Task SeedTeacherSubjectAsync(SchoolDb db, UserManager<User> userManager)
        {
            var teacher = await userManager.FindByEmailAsync("b@b.hu");
            var subject = await db.Subjects.FirstOrDefaultAsync(x => x.Name == "Informatika");

            try
            {
                if (teacher != null && subject != null)
                {
                    var ts = new TeacherSubject()
                    {
                        SubjectId = subject.Id,
                        Subject = subject,
                        TeacherId = teacher.Id,
                        Teacher = teacher
                    };

                    if (await db.Teachers.FirstOrDefaultAsync(x => x.TeacherId == ts.TeacherId && x.SubjectId == ts.SubjectId) == null)
                    {
                        await db.Teachers.AddAsync(ts);
                        await db.SaveChangesAsync();
                    }

                    else
                    {
                        Console.WriteLine("Már létezik a tanár és tantárgy");

                    }

                }

                else
                {
                    Console.WriteLine("Nem megfelelő adatok");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task SeedClassesAsync(SchoolDb db)
        {
            var cls = new Class()
            {
                Name = "12.a",
                Year = 4
            };

            var cls1 = new Class()
            {
                Name = "10.b",
                Year = 2
            };

            var result = await db.Classes.FirstOrDefaultAsync(x => x.Name == cls.Name);
            var result1 = await db.Classes.FirstOrDefaultAsync(x => x.Name == cls1.Name);

            if (result == null)
            {
                await db.Classes.AddAsync(cls);
                await db.SaveChangesAsync();
            }

            else
            {
                Console.WriteLine("Az osztály már létezik");
            }

            if (result1 == null)
            {
                await db.Classes.AddAsync(cls1);
                await db.SaveChangesAsync();
            }

            else
            {
                Console.WriteLine("Az osztály már létezik");
            }

        }

        public static async Task SeedClassSubjectsAsync(SchoolDb db)
        {
            var cls = await db.Classes.FirstOrDefaultAsync(x => x.Name == "12.a");
            var cls1 = await db.Classes.FirstOrDefaultAsync(x => x.Name == "11.b");
            var sbj = await db.Subjects.FirstOrDefaultAsync(x => x.Name == "Informatika");
            if (cls != null && sbj != null)
            {
                var cs = new ClassSubject()
                {
                    Class = cls,
                    Subject = sbj,
                    ClassId = cls.Id,
                    SubjectId = sbj.Id

                };

                if (cls1 != null && sbj != null)
                {
                    var cs1 = new ClassSubject()
                    {
                        Class = cls1,
                        Subject = sbj,
                        ClassId = cls.Id,
                        SubjectId = sbj.Id

                    };

                    var result = await db.ClassSubjects.FirstOrDefaultAsync(x => x.SubjectId == cs.SubjectId && x.ClassId == cs.ClassId);
                    var result1 = await db.ClassSubjects.FirstOrDefaultAsync(x => x.SubjectId == cs1.SubjectId && x.ClassId == cs1.ClassId);

                    if (result == null)
                    {
                        await db.ClassSubjects.AddAsync(cs);
                        await db.SaveChangesAsync();
                    }

                    else
                    {
                        Console.WriteLine($"A(z) {sbj.Name} tantárgy már hozzá van rendelve a(z){cls.Name} osztályhoz");
                    }

                    if (result1 == null)
                    {
                        await db.ClassSubjects.AddAsync(cs1);
                        await db.SaveChangesAsync();
                    }

                    else
                    {
                        Console.WriteLine($"A(z) {sbj.Name} tantárgy már hozzá van rendelve a(z){cls1.Name} osztályhoz");
                    }

                }
            }
        }
    }
}
