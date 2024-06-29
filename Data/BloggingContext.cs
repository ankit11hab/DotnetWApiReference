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
}
