using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class CityEmblem
{
    public int Id { get; set; }

    public int City { get; set; }

    public string Emblem { get; set; } = null!;

    public virtual City CityNavigation { get; set; } = null!;
}
