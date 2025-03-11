namespace e_school.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
