using Microsoft.EntityFrameworkCore;

namespace e_school.Models
{
    public class SchoolDb : DbContext
    {

        public SchoolDb(DbContextOptions<SchoolDb> db): base(db) 
        {
            
        }


    }
}
