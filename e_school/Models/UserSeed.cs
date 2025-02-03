using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_school.Models
{
    public static class UserSeed
    {
        public static async Task SeedDataAsync(SchoolDb db, UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
            await SeedGradesAsync(db);
        }
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (await userManager.FindByEmailAsync("b@b.hu") == null) 
            {
                User user = new User()
                {
                    Email = "a@a.hu",
                    BirthDate =new DateTime(1994, 12, 16),
                    PasswordHash = "5c222a477001ef63ec220dd86c27f952a55397b4987749bed0bb2568a82292ac",
                    UserName = "a@a.hu",
                    PhoneNumber = "0036309833728",
                    PhoneNumberConfirmed = true
                };

                IdentityResult result =await userManager.CreateAsync(user);
                Console.WriteLine(result);

                User user1 = new User()
                {
                    Email = "b@b.hu",
                    BirthDate = new DateTime(1994, 12, 16),
                    PasswordHash = "5c222a477001ef63ec220dd86c27f952a55397b4987749bed0bb2568a82292ac",
                    UserName = "b@b.hu",
                    PhoneNumber = "0036309833728",
                    PhoneNumberConfirmed = true
                };

                IdentityResult result1 = await userManager.CreateAsync(user1);
                Console.WriteLine(result1);
            }
        }
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                IdentityResult roleResult =await roleManager.CreateAsync(role);
                Console.WriteLine(roleResult);
            }
        }

        public static async Task SeedGradesAsync(SchoolDb db) 
        {
            User student = await db.Users.FirstOrDefaultAsync(x => x.Email == "a@a.hu");
            User teacher = await db.Users.FirstOrDefaultAsync(x => x.Email == "b@b.hu");

            Grade grade = new Grade()
            {
                Date = DateTime.Now,
                StudentId = student.Id,
                Student = student,
                TeacherId = teacher.Id,
                Value = 1

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
    }
}
