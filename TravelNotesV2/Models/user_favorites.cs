using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class user_favorites
{
    public int SpotId { get; set; }

    public int UserId { get; set; }

    public int id { get; set; }

    public virtual spots Spot { get; set; } = null!;

    public virtual users User { get; set; } = null!;
}
