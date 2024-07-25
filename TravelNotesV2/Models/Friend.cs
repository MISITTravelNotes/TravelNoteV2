using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class Friend
{
    public int uuid { get; set; }

    public int? FriendId { get; set; }

    public int? UserId { get; set; }

    public bool? Status { get; set; }
}
