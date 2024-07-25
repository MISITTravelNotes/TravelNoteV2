using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class albums
{
    public int AlbumId { get; set; }

    public int UserId { get; set; }

    public string AlbumName { get; set; } = null!;

    public DateOnly? CreateTime { get; set; }

    public int? State { get; set; }

    public virtual users User { get; set; } = null!;

    public virtual ICollection<photos> photos { get; set; } = new List<photos>();
}
