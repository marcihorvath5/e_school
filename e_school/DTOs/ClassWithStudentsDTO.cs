using e_school.Models;

namespace e_school.DTOs
{
    public class ClassWithStudentsDTO
    {
        public string ClassName { get; set;  }

        public List<string> Subjects { get; set; } = new List<string>();
        public List<StudentsWithGradesDTO>? Students { get; set; }
    }
}
