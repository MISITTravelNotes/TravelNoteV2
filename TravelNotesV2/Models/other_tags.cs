using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class other_tags
{
    public int OtherTagId { get; set; }

    public string OtherTagName { get; set; } = null!;

    public virtual ICollection<article_other_tags> article_other_tags { get; set; } = new List<article_other_tags>();
}
