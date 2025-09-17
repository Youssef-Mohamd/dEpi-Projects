using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF.Models
{
    public class InstructorCourse
    {
        // Composite key: InstructorId + CourseId -> configure in Fluent API
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public DateTime AssignedDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
