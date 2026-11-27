using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.NewsfeedService.Entities;

public class Post : FullAuditedAggregateRoot<Guid>
{
    public Guid? BranchId { get; set; }
    public Guid AuthorUserId { get; set; }
    public PostCategory Category { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? MediaUrlsJson { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public bool IsPublished { get; set; } = true;
    public DateTime? PublishedAt { get; set; }

    public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();
    public ICollection<PostComment> Comments { get; set; } = new List<PostComment>();

    protected Post() { }
}

public class PostLike : FullAuditedEntity<Guid>
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    protected PostLike() { }
}

public class PostComment : FullAuditedEntity<Guid>
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;

    protected PostComment() { }
}
