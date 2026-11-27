using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.NotificationChatService.Entities;

public class ChatRoom : FullAuditedAggregateRoot<Guid>
{
    public ChatRoomType RoomType { get; set; }
    public Guid? BranchId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ChatRoomMember> Members { get; set; } = new List<ChatRoomMember>();
    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    protected ChatRoom() { }
}

public class ChatRoomMember : FullAuditedEntity<Guid>
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime JoinedAt { get; set; }
    public Guid? LastReadMessageId { get; set; }

    protected ChatRoomMember() { }
}

public class ChatMessage : FullAuditedEntity<Guid>
{
    public Guid RoomId { get; set; }
    public Guid SenderUserId { get; set; }
    public ChatMessageType MessageType { get; set; } = ChatMessageType.Text;
    public string Content { get; set; } = string.Empty;
    public string? AttachmentUrl { get; set; }
    public DateTime SentAt { get; set; }

    protected ChatMessage() { }
}

public class Notification : FullAuditedAggregateRoot<Guid>
{
    public Guid RecipientUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }

    protected Notification() { }
}
