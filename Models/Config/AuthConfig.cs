using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Api;

public class AuthConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
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
        
        builder
            .HasData(
                new Person { Id = 2, 
                    Name = "admin", 
                    Email = "admin@example.com", 
                    Password = BCrypt.Net.BCrypt.HashPassword("admin") }
            );
    }
}
