using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class DirectionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Juror> Jurors { get; set; } = new List<Juror>();

    public virtual ICollection<Moderator> Moderators { get; set; } = new List<Moderator>();
}
