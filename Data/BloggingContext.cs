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

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Roles)
            .WithMany(r => r.Persons)
            .UsingEntity<Dictionary<string, object>>(
                "PersonRole",
                pr => pr.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                pr => pr.HasOne<Person>().WithMany().HasForeignKey("PersonId"),
                pr =>
                {
                    pr.HasKey("PersonId", "RoleId");

                    // Seed data for the join table
                    pr.HasData(
                        new { PersonId = 2, RoleId = "User" },
                        new { PersonId = 2, RoleId = "Admin" }
                    );
                });

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = "User" },
            new Role { Id = "Admin" }
        );

        // Seed Persons
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 2, Name = "admin", Email = "admin@example.com", Password = BCrypt.Net.BCrypt.HashPassword("admin") }
        );
    }
}
