using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class users
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public string? Gender { get; set; }

    public string? Pwd { get; set; }

    public string? Nickname { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Address { get; set; }

    public string? Introduction { get; set; }

    public string? Interest { get; set; }

    public string? Headshot { get; set; }

    public string? SuperUser { get; set; }

    public virtual ICollection<albums> albums { get; set; } = new List<albums>();

    public virtual ICollection<articles> articles { get; set; } = new List<articles>();

    public virtual ICollection<look_backs> look_backs { get; set; } = new List<look_backs>();

    public virtual ICollection<message_boards> message_boards { get; set; } = new List<message_boards>();

    public virtual ICollection<photos> photos { get; set; } = new List<photos>();

    public virtual ICollection<user_favorites> user_favorites { get; set; } = new List<user_favorites>();
}
