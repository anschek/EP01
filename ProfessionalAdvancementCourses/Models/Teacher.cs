using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Teacher
{
    public Guid Id { get; set; }

    public int Experience { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Teacherslesson> Teacherslessons { get; set; } = new List<Teacherslesson>();
}
