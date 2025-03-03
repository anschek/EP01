using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class Event
{
    public int Id { get; set; }

    public int Activity { get; set; }

    public string Description { get; set; } = null!;

    public int DayNumber { get; set; }

    public DateTime StartTime { get; set; }

    public int Moderator { get; set; }

    public virtual Activity ActivityNavigation { get; set; } = null!;

    public virtual Moderator ModeratorNavigation { get; set; } = null!;

    public virtual ICollection<Juror> Juries { get; set; } = new List<Juror>();
}
