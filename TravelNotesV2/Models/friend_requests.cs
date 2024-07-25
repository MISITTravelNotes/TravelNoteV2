using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class friend_requests
{
    public int uuid { get; set; }

    public int? SenderUserId { get; set; }

    public int? ReceiverUserId { get; set; }

    public int? Status { get; set; }
}
