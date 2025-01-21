namespace e_school.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int TeacherId { get; set; }
        public User Teacher { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }
    }
}