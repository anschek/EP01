using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Teacherload
{
    public int Id { get; set; }

    public int? TeacherLessonId { get; set; }

    public int Hours { get; set; }

    public DateTime StartOfPeriod { get; set; }

    public DateTime EndOfPeriod { get; set; }

    public virtual Teacherslesson? TeacherLesson { get; set; }
}
