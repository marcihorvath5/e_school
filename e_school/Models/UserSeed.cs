using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_school.Models
{
    public static class UserSeed
    {
        public static async Task SeedDataAsync(SchoolDb db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //await SeedRolesAsync(roleManager);
            //await SeedClassesAsync(db);
            //await SeedStudentsAsync(userManager, db);
            //await SeedTeachersAsync(userManager);
            //await SeedSubjectsAsync(db);
            //await SeedClassSubjectsAsync(db);
            //await SeedTeacherSubjectsAsync(db, userManager);            
            //await SeedGradesAsync(db);          
            //await SeedClassTeacherSubjectsAsync(db, userManager);
        }

        public static async Task SeedStudentsAsync(UserManager<User> userManager, SchoolDb db)
        {
            var classes = await db.Classes.ToListAsync();
            var firstNames = new List<string> { "Bence", "Anna", "Gabor", "Eszter", "Laszlo", "Katalin", "Miklos", "Zsofia", "Istvan", "Julia" };
            var lastNames = new List<string> { "Kovacs", "Nagy", "Toth", "Szabo", "Horvath", "Varga", "Kiss", "Molnar", "Nemeth", "Farkas" };
            int studentCounter = 1;
            var random = new Random();

            foreach (var cls in classes)
            {
                for (int i = 0; i < 10; i++)
                {
                    string firstName = firstNames[random.Next(firstNames.Count)];
                    string lastName = lastNames[random.Next(lastNames.Count)];
                    string email = $"{firstName.ToLower()}.{lastName.ToLower()}{studentCounter}@school.com";
                    if (await userManager.FindByEmailAsync(email) == null)
                    {
                        User user = new User()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            BirthDate = new DateOnly(2005, 1, 1).AddYears(-cls.Year),
                            UserName = email,
                            PhoneNumber = $"003630000000{studentCounter}",
                            PhoneNumberConfirmed = true,
                            ClassId = cls.Id,
                            EmailConfirmed = true,
                            TwoFactorEnabled = false,
                            LockoutEnabled = false,
                            AccessFailedCount = 0
                        };

                        IdentityResult result = await userManager.CreateAsync(user, "Password123!");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "Student");
                            Console.WriteLine($"User created: {email}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to create user: {email}");
                        };

                        studentCounter++;
                    }
                }
            } 
        }

        public static async Task SeedTeachersAsync(UserManager<User> userManager)
        {
            var firstNames = new List<string> { "Andras", "Balazs", "Csaba", "Dorottya", "Erika", "Ferenc", "Gyorgy", "Hajnalka", "Imre", "Janos" };
            var lastNames = new List<string> { "Balogh", "Fazekas", "Gulyas", "Hegedus", "Juhasz", "Kertesz", "Lakatos", "Madarasz", "Nagy", "Orban" };
            int teacherCounter = 1;
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Count)];
                string lastName = lastNames[random.Next(lastNames.Count)];
                string email = $"{firstName.ToLower()}.{lastName.ToLower()}{teacherCounter}@school.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    User user = new User()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        BirthDate = new DateOnly(1980, 1, 1).AddYears(random.Next(20)),
                        UserName = email,
                        PhoneNumber = $"003630000000{teacherCounter}",
                        PhoneNumberConfirmed = true
                    };

                    IdentityResult result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Teacher");
                        Console.WriteLine($"Teacher created: {email}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create teacher: {email}");
                    }
                }
                teacherCounter++;
            }
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = ["Student", "Teacher", "Admin", "Unclassified"];

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


        public static async Task SeedTeacherSubjectsAsync(SchoolDb db, UserManager<User> userManager)
        {
            var teachers = await userManager.GetUsersInRoleAsync("Teacher");
            var subjects = await db.Subjects.ToListAsync();
            var random = new Random();

            // Biztosítjuk, hogy minden tantárgy legalább egyszer kiosztásra kerüljön
            var subjectQueue = new Queue<Subject>(subjects.OrderBy(_ => random.Next()));

            foreach (var teacher in teachers)
            {
                for (int i = 0; i < 2; i++)
                {
                    Subject subject;

                    if (subjectQueue.Count > 0)
                    {
                        subject = subjectQueue.Dequeue();
                    }
                    else
                    {
                        subject = subjects[random.Next(subjects.Count)];
                    }

                    var teacherSubject = new TeacherSubject
                    {
                        TeacherId = teacher.Id,
                        SubjectId = subject.Id
                    };

                    if (await db.Teachers.FirstOrDefaultAsync(ts => ts.TeacherId == teacherSubject.TeacherId && ts.SubjectId == teacherSubject.SubjectId) == null)
                    {
                        await db.Teachers.AddAsync(teacherSubject);
                        await db.SaveChangesAsync();
                        Console.WriteLine($"Assigned subject {subject.Name} to teacher {teacher.Email}");
                    }
                    else
                    {

                        Console.WriteLine($"Subject {subject.Name} already assigned to teacher {teacher.Email}");
                    }
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
            var subjects = new List<Subject>
            {
                new Subject { Name = "Matematika" },
                new Subject { Name = "Fizika" },
                new Subject { Name = "Kemia" },
                new Subject { Name = "Biológia" },
                new Subject { Name = "Informatika" },
                new Subject { Name = "Torténelem" },
                new Subject { Name = "Foldrajz" },
                new Subject { Name = "Irodalom" },
                new Subject { Name = "Nyelvtan" },
                new Subject { Name = "Angol" },
                new Subject { Name = "Nemet" },
                new Subject { Name = "Francia" }
            };

            foreach (var subject in subjects)
            {
                var existingSubject = await db.Subjects.FirstOrDefaultAsync(x => x.Name == subject.Name);
                if (existingSubject == null)
                {
                    await db.Subjects.AddAsync(subject);
                    await db.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"A tantárgy már létezik: {subject.Name}");
                }
            }

        }        

        public static async Task SeedClassesAsync(SchoolDb db)
        {
            var classes = new List<Class>
    {
        new Class { Name = "9.a", Year = 1 },
        new Class { Name = "9.b", Year = 1 },
        new Class { Name = "9.c", Year = 1 },
        new Class { Name = "10.a", Year = 2 },
        new Class { Name = "10.b", Year = 2 },
        new Class { Name = "10.c", Year = 2 },
        new Class { Name = "11.a", Year = 3 },
        new Class { Name = "11.b", Year = 3 },
        new Class { Name = "11.c", Year = 3 },
        new Class { Name = "12.a", Year = 4 },
        new Class { Name = "12.b", Year = 4 },
        new Class { Name = "12.c", Year = 4 }
    };

            foreach (var cls in classes)
            {
                var existingClass = await db.Classes.FirstOrDefaultAsync(x => x.Name == cls.Name);
                if (existingClass == null)
                {
                    await db.Classes.AddAsync(cls);
                    await db.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"Az osztály már létezik: {cls.Name}");
                }
            }

        }

        public static async Task SeedClassSubjectsAsync(SchoolDb db)
        {
            var classes = await db.Classes.ToListAsync();
            var subjects = await db.Subjects.ToListAsync();
            var random = new Random();

            foreach (var cls in classes)
            {
                var assignedSubjects = new HashSet<int>();

                for (int i = 0; i < 8; i++)
                {
                    Subject subject;
                    do
                    {
                        subject = subjects[random.Next(subjects.Count)];
                    } while (assignedSubjects.Contains(subject.Id));

                    assignedSubjects.Add(subject.Id);

                    var classSubject = new ClassSubject
                    {
                        ClassId = cls.Id,
                        SubjectId = subject.Id
                    };

                    if (await db.ClassSubjects.FirstOrDefaultAsync(cs => cs.ClassId == classSubject.ClassId && cs.SubjectId == classSubject.SubjectId) == null)
                    {
                        await db.ClassSubjects.AddAsync(classSubject);
                        await db.SaveChangesAsync();
                        Console.WriteLine($"Assigned subject {subject.Name} to class {cls.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Subject {subject.Name} already assigned to class {cls.Name}");
                    }
                }
            }
        }

        public static async Task SeedClassTeacherSubjectsAsync(SchoolDb db, UserManager<User> userManager)
        {
            var classes = await db.Classes.ToListAsync();
            var random = new Random();

            foreach (var cls in classes)
            {
                var classSubjects = await db.ClassSubjects.Where(cs => cs.ClassId == cls.Id).ToListAsync();

                foreach (var classSubject in classSubjects)
                {
                    var teachers = await db.Teachers.Where(ts => ts.SubjectId == classSubject.SubjectId).ToListAsync();

                    if (teachers.Count > 0)
                    {
                        var teacher = teachers[random.Next(teachers.Count)];

                        var classTeacherSubject = new ClassTeacherSubject
                        {
                            ClassId = cls.Id,
                            SubjectId = classSubject.SubjectId,
                            TeacherId = teacher.TeacherId
                        };

                        if (await db.ClassTeacherSubjects.FirstOrDefaultAsync(cts => cts.ClassId == classTeacherSubject.ClassId && cts.SubjectId == classTeacherSubject.SubjectId && cts.TeacherId == classTeacherSubject.TeacherId) == null)
                        {
                            await db.ClassTeacherSubjects.AddAsync(classTeacherSubject);
                            await db.SaveChangesAsync();
                            Console.WriteLine($"Assigned teacher  to class  for subject ");
                        }
                        else
                        {
                            Console.WriteLine($"Teacher  already assigned to class  for subject ");
                        }
                    }
                }
            }
        }
    }
}
