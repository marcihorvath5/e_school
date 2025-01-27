using Microsoft.AspNetCore.Identity;

namespace e_school.Models
{
    public class User: IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public ICollection<User>? Children { get; set; }

        public string? ParentId { get; set; }
        public User? Parent { get; set; }

        public int? ClassId { get; set; }
        public Class? Class { get; set; }

        public ICollection<Grade>? Grades { get; set; }
        public ICollection<TeacherSubject>? Subjects { get; set; }
        public ICollection<Grade>? GivenGrades { get; set; }
        public ICollection<ClassTeacherSubject>? ClassAndSubject {  get; set; }
    }
}
