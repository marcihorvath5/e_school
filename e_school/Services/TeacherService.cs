using e_school.DTOs;
using e_school.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_school.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolDb _db;
        private readonly UserManager<User> _userManager;

        public TeacherService(SchoolDb db, UserManager<User> usermanager)
        {
            _db = db;
            _userManager = usermanager;
        }

        public  ClassWithStudentsDTO GetClassByName(string name)
        {
            var result =  _db.Classes
                .Where(c => c.Name == name)
                .Include(c => c.Subjects)
                    .ThenInclude(s => s.Subject)
                .Include(c => c.Students)
                .ThenInclude(g => g.Grades)
                    .ThenInclude(s => s.Subject)
                .AsEnumerable()
                .Select(c => new ClassWithStudentsDTO
                {
                    ClassName = c.Name,
                    Subjects = c.Subjects.Select(s => s.Subject.Name).ToList(),
                    Students = c.Students.Select(s => new StudentsWithGradesDTO
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Grades = s.Grades
                            .GroupBy(g => g.Subject.Name)
                            .Select(group => new GradesWithSubjectDTO
                            {
                                SubjectName = group.Key,
                                Grades = group.Select(g => new GradeDTO 
                                {
                                    GradeId = g.Id,
                                    GradeValue = g.Value,
                                    Date = g.Date
                                }).ToList()
                            }).ToList()

                    }).ToList()
                }).FirstOrDefault();

            if (result != null) 
            {
                return result;
            }

            return null;
        }

        public List<Class> GetClasses()
        {
            List<Class> classes = _db.Classes.ToList();

            return classes;
        }

        public async Task<bool> DeleteGradeAsync(int gradeId, string studentId)
        {
            var grade =await _db.Grades
                .Where(g => g.Id == gradeId && g.StudentId == studentId)
                .FirstOrDefaultAsync();

            if (grade != null)
            {
                _db.Grades.Remove(grade);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ModifyGradeAsync(int gradeId, int newValue)
        {
            var grade = await _db.Grades
                            .Where(g => g.Id == gradeId)
                            .FirstOrDefaultAsync();

            if (grade != null)
            {
                grade.Value = newValue;
                grade.Date = DateTime.UtcNow;

                _db.Update(grade);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> AddGradeAsync(int value, string studentId, string teacherId, int subjectId)
        {
            var student = await _db.Users.FirstOrDefaultAsync(s => s.Id == studentId);
            var teacher = await _db.Users.FirstOrDefaultAsync(t => t.Id == teacherId);
            var subject = await _db.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);

            if (student != null && teacher != null && subject != null && 0 < value && value <= 5)
            {
                Grade grade = new Grade() 
                {
                    Student = student,
                    StudentId = student.Id,
                    Subject = subject,
                    SubjectId = subject.Id,
                    Teacher = teacher,
                    TeacherId = teacher.Id,
                    Value = value,
                    Date = DateTime.UtcNow
                };

                await _db.Grades.AddAsync(grade);
                await _db.SaveChangesAsync();

                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
