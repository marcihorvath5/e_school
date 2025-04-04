namespace e_school.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public ICollection<User> Students { get; set; } = new List<User>();
        public ICollection<ClassSubject> Subjects { get; set; } = new List<ClassSubject>();
        public ICollection<ClassTeacherSubject> TeacherAndSubject { get; set; } = new List<ClassTeacherSubject>();
    }
}