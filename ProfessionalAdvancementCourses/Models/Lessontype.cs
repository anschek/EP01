using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Lessontype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
