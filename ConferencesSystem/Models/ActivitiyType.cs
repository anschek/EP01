using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class ActivitiyType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Moderator> Moderators { get; set; } = new List<Moderator>();
}
