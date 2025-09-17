using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; }    // Unique -> configure in Fluent API

        [Required, MaxLength(20)]
        public string StudentNumber { get; set; } // Unique -> Fluent API

        [Required]
        public DateTime EnrollmentDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();
    }

}
