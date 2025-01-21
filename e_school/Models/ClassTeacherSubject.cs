namespace e_school.Models
{
    public class ClassTeacherSubject
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int TeacherId { get; set; }
        public TeacherSubject Teacher { get; set; }

        public int SubjectId {  get; set; }
        public TeacherSubject Subject { get; set; }       
    }
}