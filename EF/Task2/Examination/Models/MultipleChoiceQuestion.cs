using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class MultipleChoiceQuestion : Question
    {
        [Required, MaxLength(500)] public string OptionA { get; set; }
        [Required, MaxLength(500)] public string OptionB { get; set; }
        [Required, MaxLength(500)] public string OptionC { get; set; }
        [Required, MaxLength(500)] public string OptionD { get; set; }

        [Required, MaxLength(1)]
        public string CorrectOption { get; set; } 
    }

}
