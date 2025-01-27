using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_school.Models
{
    public class SchoolDb : IdentityDbContext<User>
    {

        public SchoolDb(DbContextOptions<SchoolDb> db): base(db) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<TeacherSubject> Teachers { get; set; }
        public DbSet<ClassTeacherSubject> ClassTeacherSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClassSubject>().HasKey(x => new {x.SubjectId, x.ClassId});
            builder.Entity<ClassSubject>().HasOne(s => s.Subject)
                                          .WithMany(s => s.Subjects);

            builder.Entity<ClassSubject>().HasOne(c => c.Class)
                                          .WithMany(c => c.Classes);
            
            builder.Entity<TeacherSubject>().HasKey(x => new {x.SubjectId, x.TeacherId});
            builder.Entity<TeacherSubject>().HasOne(t => t.Teacher)
                                            .WithMany(s => s.Subjects);

            builder.Entity<TeacherSubject>().HasOne(s => s.Subject)
                                            .WithMany(su => su.Teachers);

            builder.Entity<ClassTeacherSubject>().HasKey(x => new {x.SubjectId, x.TeacherId, x.ClassId});
            builder.Entity<ClassTeacherSubject>().HasOne(s => s.Subject)
                                                 .WithMany(ts => ts.ClassAndTeacher);

            builder.Entity<ClassTeacherSubject>().HasOne(s => s.Teacher)
                                     .WithMany(ts => ts.ClassAndSubject);

            builder.Entity<ClassTeacherSubject>().HasOne(c => c.Class)
                                                 .WithMany(ts => ts.TeacherAndSubject);

            builder.Entity<Grade>().HasOne(s => s.Student)
                                   .WithMany(g => g.Grades)
                                   .HasForeignKey(g => g.StudentId);
            builder.Entity<Grade>().HasOne(t => t.Teacher)
                                   .WithMany(g => g.GivenGrades)
                                   .HasForeignKey(g =>g.TeacherId);        
        }
    }
}
