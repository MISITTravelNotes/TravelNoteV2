using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class article_other_tags
{
    public int Id { get; set; }

    public int ArticleId { get; set; }

    public int OtherTagId { get; set; }

    public virtual articles Article { get; set; } = null!;

    public virtual other_tags OtherTag { get; set; } = null!;
}
