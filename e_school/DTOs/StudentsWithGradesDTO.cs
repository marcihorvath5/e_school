namespace e_school.DTOs
{
    public class StudentsWithGradesDTO
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<GradesWithSubjectDTO>? Grades { get; set; }
    }
}
