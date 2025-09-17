using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class StudentAnswer
    {
        public int Id { get; set; }

        [MaxLength(2000)]
        public string AnswerText { get; set; }  // essays

        [MaxLength(1)]
        public string SelectedOption { get; set; }  // "A"/"B"/...

        public bool? BooleanAnswer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MarksObtained { get; set; }

        [Required]
        public DateTime SubmittedAt { get; set; }

        // FKs
        [Required]
        public int ExamAttemptId { get; set; }
        public ExamAttempt ExamAttempt { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }

}
