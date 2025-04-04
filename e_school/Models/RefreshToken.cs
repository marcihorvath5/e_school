namespace e_school.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
