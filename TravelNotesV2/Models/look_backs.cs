using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class look_backs
{
    public int Yid { get; set; }

    public int UserId { get; set; }

    public int PhotoId { get; set; }

    public int id { get; set; }

    public virtual photos Photo { get; set; } = null!;

    public virtual users User { get; set; } = null!;
}
