namespace e_school.Models
{
    public class TeacherSubject
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public string TeacherId { get; set; }
        public User Teacher { get; set; }
    }
}