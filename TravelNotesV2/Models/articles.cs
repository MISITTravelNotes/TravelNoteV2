using System;
using System.Collections.Generic;

namespace TravelNotesV2.Models;

public partial class articles
{
    public int ArticleId { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public DateTime? PublishTime { get; set; }

    public DateTime? TravelTime { get; set; }

    public string? Contents { get; set; }

    public string? Images { get; set; }

    public int? LikeCount { get; set; }

    public int? PageView { get; set; }

    public string? ArticleState { get; set; }

    public int? SpotId { get; set; }

    public virtual users User { get; set; } = null!;

    public virtual ICollection<article_other_tags> article_other_tags { get; set; } = new List<article_other_tags>();

    public virtual ICollection<message_boards> message_boards { get; set; } = new List<message_boards>();
}
