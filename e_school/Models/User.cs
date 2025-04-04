using Microsoft.AspNetCore.Identity;

namespace e_school.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public int? ClassId { get; set; }
        public Class? Class { get; set; }

        public ICollection<Grade>? Grades { get; set; }
        public ICollection<TeacherSubject>? Subjects { get; set; }
        public ICollection<Grade>? GivenGrades { get; set; }
        public ICollection<ClassTeacherSubject>? ClassAndSubject {  get; set; }
    }
}
