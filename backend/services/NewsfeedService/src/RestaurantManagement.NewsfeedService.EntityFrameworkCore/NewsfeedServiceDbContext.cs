using Microsoft.EntityFrameworkCore;
using RestaurantManagement.NewsfeedService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.NewsfeedService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class NewsfeedServiceDbContext : AbpDbContext<NewsfeedServiceDbContext>
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostLike> PostLikes => Set<PostLike>();
    public DbSet<PostComment> PostComments => Set<PostComment>();

    public NewsfeedServiceDbContext(DbContextOptions<NewsfeedServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>(b =>
        {
            b.ToTable("Posts");
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(300);
            b.Property(x => x.Content).IsRequired();
            b.HasMany(x => x.Likes).WithOne().HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PostLike>(b =>
        {
            b.ToTable("PostLikes");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.PostId, x.UserId }).IsUnique();
        });

        builder.Entity<PostComment>(b =>
        {
            b.ToTable("PostComments");
            b.ConfigureByConvention();
            b.Property(x => x.Content).IsRequired().HasMaxLength(2000);
        });
    }
}
