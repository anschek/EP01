using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Student
{
    public Guid Id { get; set; }

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User IdNavigation { get; set; } = null!;
}
