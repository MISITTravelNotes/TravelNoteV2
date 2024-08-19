using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class model_list
{
    public int modelId { get; set; }

    public string? modelName { get; set; }

    public int? useCount { get; set; }
}
