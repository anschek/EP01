using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class User
{
    public User() 
    {
        FullName = "";
        Mail = "";
        Country = 171;
        PhoneNumber = "";
        Password = "";
        Activities = new List<Activity>();
    }
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public int Country { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Gender { get; set; }

    public int Role { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual Country CountryNavigation { get; set; } = null!;

    public virtual Gender GenderNavigation { get; set; } = null!;

    public virtual Juror? Juror { get; set; }

    public virtual Moderator? Moderator { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;
}
