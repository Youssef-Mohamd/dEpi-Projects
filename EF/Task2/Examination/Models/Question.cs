using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public abstract class Question
    {
        public int Id { get; set; }

        [Required, MaxLength(1000)]
        public string QuestionText { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Marks { get; set; } // check > 0 in Fluent API

        [Required]
        public QuestionType QuestionType { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        // FK
        [Required]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        // Navigation
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }

}
