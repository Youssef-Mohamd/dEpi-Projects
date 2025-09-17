using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class StudentCourse
    {
        // Composite key: StudentId + CourseId -> configure in Fluent API
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Grade { get; set; }

        public bool IsCompleted { get; set; } = false;
    }

}
