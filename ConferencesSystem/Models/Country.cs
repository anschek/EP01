using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class Country
{
    public int Id { get; set; }

    public string NameRu { get; set; } = null!;

    public string NameEng { get; set; } = null!;

    public string LiteralCode { get; set; } = null!;

    public int NumericCode { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
