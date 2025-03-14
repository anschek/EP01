using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }
    public override string ToString() =>
        $"{SecondName} {FirstName} {MiddleName}";
}
