﻿using System;
using System.Collections.Generic;

namespace ConferencesSystem.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
