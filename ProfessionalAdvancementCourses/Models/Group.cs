using System;
using System.Collections.Generic;

namespace ProfessionalAdvancementCourses.Models;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? SpecialityId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Lessonsgroup> Lessonsgroups { get; set; } = new List<Lessonsgroup>();

    public virtual Specialty? Speciality { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
