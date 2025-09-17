using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class Exam
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalMarks { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign keys
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        // Navigation
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();
    }

}
