using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
    {

    }
}
