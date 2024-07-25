using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class photos
{
    public int PhotoId { get; set; }

    public string PhotoTitle { get; set; } = null!;

    public string? PhotoDescription { get; set; }

    public string? PhotoPath { get; set; }

    public DateOnly? UploadDate { get; set; }

    public int? AlbumId { get; set; }

    public int UserId { get; set; }

    public virtual albums? Album { get; set; }

    public virtual users User { get; set; } = null!;

    public virtual ICollection<look_backs> look_backs { get; set; } = new List<look_backs>();
}
