namespace e_school.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<TeacherSubject> Teachers { get; set; }
        public ICollection<ClassSubject> Subjects { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<ClassTeacherSubject> ClassAndTeacher { get; set; }
    }
}