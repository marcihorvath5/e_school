namespace e_school.Models
{
    public class ClassTeacherSubject
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public string TeacherId { get; set; }
        public User Teacher { get; set; }

        public int SubjectId {  get; set; }
        public Subject Subject { get; set; }       
    }
}