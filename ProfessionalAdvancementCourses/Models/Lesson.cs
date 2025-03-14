using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public int? SubjectId { get; set; }

    public int? LessonTypeId { get; set; }

    public decimal HourlyRate { get; set; }

    public int Hours { get; set; }

    public virtual Lessontype? LessonType { get; set; }

    public virtual ICollection<Lessonsgroup> Lessonsgroups { get; set; } = new List<Lessonsgroup>();

    public virtual Subject? Subject { get; set; }

    public override string ToString() =>
        $"{Subject?.Name} - {LessonType?.Name}"; 
    
}
