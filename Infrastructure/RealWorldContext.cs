using Microsoft.EntityFrameworkCore;
using RealWorld.Domain;
using RealWorld.Infrastructure.Security;

namespace RealWorld.Infrastructure;

public class RealWorldContext : DbContext
{
    IPasswordHasher _passwordHasher;
    public RealWorldContext(DbContextOptions options, IPasswordHasher passwordHasher) : base(options)
    {
        _passwordHasher = passwordHasher;
    }

    public DbSet<Article> Articles { get; set; } = null!;
    /* public DbSet<ArticleTag> ArticleTags { get; set; } = null!; */
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasKey(b => b.Name);

        modelBuilder.Entity<Article>(b =>
        {
            b.HasKey(t => t.Id);
            b.HasMany(t => t.Tags)
                .WithMany(t => t.Articles)
                .UsingEntity(j => j.ToTable("ArticleTags"));
            b.HasMany(t => t.FavoriteUsers)
                .WithMany(t => t.FavoriteArticles)
                .UsingEntity(j => j.ToTable("ArticleFavorites"));
            b.HasOne(t => t.Author)
                .WithMany(t => t.CreatedArticles);
        });

        modelBuilder.Entity<User>()
            .HasMany(x => x.Followings)
            .WithMany(x => x.Followers)
            .UsingEntity(j => j.ToTable("UserFollowers"));

        SeedData(modelBuilder);
    }
    void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(new Tag { Name = "HelloWorld" });
        modelBuilder.Entity<Tag>().HasData(new Tag { Name = "lsopaslkj" });
        modelBuilder.Entity<Tag>().HasData(new Tag { Name = "HoangTheTrung" });
        modelBuilder.Entity<Tag>().HasData(new Tag { Name = "hocChoGioi" });
        modelBuilder.Entity<Tag>().HasData(new Tag { Name = "LonRoiConHayKhocNhe" });
        modelBuilder.Entity<Tag>().HasData(new Tag { Name = "ChauLenBa" });

        var salt = Guid.NewGuid().ToByteArray();
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Salt = salt,
                Email = "wanbicoi123@gmail.com",
                UserName = "Victor",
                Hash = _passwordHasher.Hash("songbien", salt),
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 2,
                Salt = salt,
                Email = "wan@gmail.com",
                UserName = "SongGa",
                Hash = _passwordHasher.Hash("songbien", salt),
            }
        );

        /* modelBuilder.Entity<Article>().HasData( */
        /*     new Article */
        /*     { */
        /*         Id = 1, */
        /*         Slug = "how-to-know-the-world", */
        /*         Title = "how-to-know-the-world", */
        /*         Description = "Ăn học cho đàng hoàng là biết thôi", */
        /*         Body = "ẻ chảy", */
        /*         CreatedAt = new DateTime(2008, 20, 3), */
        /*         UpdatedAt = new DateTime(2010, 21, 4), */
        /*         AuthorId = 1, */
        /*     } */
        /* ); */
    }
}
