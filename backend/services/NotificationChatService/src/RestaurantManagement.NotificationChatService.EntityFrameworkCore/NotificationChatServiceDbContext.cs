using Microsoft.EntityFrameworkCore;
using RestaurantManagement.NotificationChatService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.NotificationChatService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class NotificationChatServiceDbContext : AbpDbContext<NotificationChatServiceDbContext>
{
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    public DbSet<ChatRoomMember> ChatRoomMembers => Set<ChatRoomMember>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public NotificationChatServiceDbContext(DbContextOptions<NotificationChatServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ChatRoom>(b =>
        {
            b.ToTable("ChatRooms");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.HasMany(x => x.Members).WithOne().HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ChatRoomMember>(b =>
        {
            b.ToTable("ChatRoomMembers");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.RoomId, x.UserId }).IsUnique();
        });

        builder.Entity<ChatMessage>(b =>
        {
            b.ToTable("ChatMessages");
            b.ConfigureByConvention();
            b.Property(x => x.Content).IsRequired().HasMaxLength(4000);
            b.Property(x => x.AttachmentUrl).HasMaxLength(500);
        });

        builder.Entity<Notification>(b =>
        {
            b.ToTable("Notifications");
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(250);
            b.Property(x => x.Body).IsRequired().HasMaxLength(1000);
            b.HasIndex(x => new { x.RecipientUserId, x.IsRead });
        });
    }
}
