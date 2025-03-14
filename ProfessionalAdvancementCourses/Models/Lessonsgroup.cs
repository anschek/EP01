using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Lessonsgroup
{
    public int Id { get; set; }

    public int? LessonId { get; set; }

    public int? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Lesson? Lesson { get; set; }

    public virtual ICollection<Teacherslesson> Teacherslessons { get; set; } = new List<Teacherslesson>();
}
