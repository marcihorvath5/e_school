namespace e_school.DTOs
{
    public class StudentsWithGradesDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public List<GradesWithSubjectDTO>? Grades { get; set; }
    }
}
