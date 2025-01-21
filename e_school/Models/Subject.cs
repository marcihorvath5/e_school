namespace e_school.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<User> Teachers { get; set; }
        public ICollection<ClassSubject> Subjects { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}