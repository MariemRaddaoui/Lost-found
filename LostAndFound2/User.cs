using System;
using System.Collections.Generic;

namespace LostAndFound2;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long Phone { get; set; }
}
