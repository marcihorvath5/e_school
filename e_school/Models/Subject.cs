using Microsoft.EntityFrameworkCore;

namespace e_school.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<TeacherSubject> Teachers { get; set; } //= new List<TeacherSubject>();
        public ICollection<ClassSubject> Subjects { get; set; } //= new List<ClassSubject>();
        public ICollection<Grade> Grades { get; set; } //= new List<Grade>();
        public ICollection<ClassTeacherSubject> ClassAndTeacher { get; set; }// = new List<ClassTeacherSubject>();
    }
}