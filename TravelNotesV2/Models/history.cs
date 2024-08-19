using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class history
{
    public int id { get; set; }

    public string? interest1 { get; set; }

    public string? interest2 { get; set; }

    public string? interest3 { get; set; }

    public string? country { get; set; }

    public string? result { get; set; }

    public int? modelId { get; set; }
}
