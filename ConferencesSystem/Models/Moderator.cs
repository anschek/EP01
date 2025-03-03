using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class Moderator
{
    public int User { get; set; }

    public int DirectionType { get; set; }

    public int ActivityType { get; set; }

    public virtual ActivitiyType ActivityTypeNavigation { get; set; } = null!;

    public virtual DirectionType DirectionTypeNavigation { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual User UserNavigation { get; set; } = null!;
}
