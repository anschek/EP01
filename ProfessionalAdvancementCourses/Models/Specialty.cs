using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Specialty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
