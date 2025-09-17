using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class ExamAttempt
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalScore { get; set; }

        public bool IsSubmitted { get; set; } = false;
        public bool IsGraded { get; set; } = false;

        // FKs
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        // Navigation
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }

}
