using e_school.DTOs;
using e_school.Models;

namespace e_school.Services
{
    public interface ITeacherService
    {
        public List<Class> GetClasses();
        public ClassWithStudentsDTO GetClassByName(string name);
        public Task<bool> DeleteGradeAsync(int gradeId, string studentId);
        public Task<bool> ModifyGradeAsync(int gradeId, int newValue);
        public Task<bool> AddGradeAsync(int value, string studentId, string teacherId, int subjectId);
    }
}
