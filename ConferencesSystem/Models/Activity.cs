using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class Activity
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int DurationInDays { get; set; }

    public int City { get; set; }

    public int ActivityType { get; set; }

    public int? Winner { get; set; }

    public string? Image { get; set; }

    public virtual ActivitiyType ActivityTypeNavigation { get; set; } = null!;

    public virtual City CityNavigation { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual User? WinnerNavigation { get; set; }
}
