namespace e_school.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public ICollection<User>? Students { get; set; }
        public ICollection<ClassSubject> Classes { get; set; }
        public ICollection<ClassTeacherSubject> TeacherAndSubject { get; set; }
    }
}