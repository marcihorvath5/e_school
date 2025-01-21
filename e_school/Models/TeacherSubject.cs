﻿namespace e_school.Models
{
    public class TeacherSubject
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int TeacherId { get; set; }
        public User Teacher { get; set; }

        public ICollection<ClassTeacherSubject> Classes { get; set; }
    }
}