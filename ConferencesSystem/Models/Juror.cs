using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class Juror
{
    public int User { get; set; }

    public int Direction { get; set; }

    public virtual DirectionType DirectionNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
