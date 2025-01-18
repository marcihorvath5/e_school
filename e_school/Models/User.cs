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


    }
}
