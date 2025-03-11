using e_school.Models;

namespace e_school.DTOs
{
    public class ClassWithStudentsDTO
    {
        public string ClassName { get; set;  }
        public List<StudentsWithGradesDTO>? Students { get; set; }
    }
}
