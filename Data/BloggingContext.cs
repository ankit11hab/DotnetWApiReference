using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
    {
        
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<PersonPhoto> PersonPhotos { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new AuthConfig().Configure(modelBuilder.Entity<Person>());
        new RoleConfig().Configure(modelBuilder.Entity<Role>());
    }
}
