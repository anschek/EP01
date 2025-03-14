using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int? TeacherLessonId { get; set; }

    public DateTime DateTime { get; set; }

    public virtual Teacherslesson? TeacherLesson { get; set; }
}
