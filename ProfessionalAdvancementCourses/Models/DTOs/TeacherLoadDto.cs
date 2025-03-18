using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalAdvancementCourses.Models.DTOs
{
    public class TeacherLoadDto
    {
        public string Group {  get; set; }
        public string Lesson { get; set; }
        public int Hours { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
