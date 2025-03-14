using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Teacherslesson
{
    public int Id { get; set; }

    public Guid? TeacherId { get; set; }

    public int? LessonGroupId { get; set; }

    public virtual Lessonsgroup? LessonGroup { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual Teacher? Teacher { get; set; }

    public virtual ICollection<Teacherload> Teacherloads { get; set; } = new List<Teacherload>();
}
