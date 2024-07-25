using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class message_boards
{
    public int MessageId { get; set; }

    public int ArticleId { get; set; }

    public int UserId { get; set; }

    public string? Contents { get; set; }

    public DateTime? MessageTime { get; set; }

    public virtual articles Article { get; set; } = null!;

    public virtual users User { get; set; } = null!;
}
