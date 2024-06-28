using System.ComponentModel.DataAnnotations;

namespace Blog.Api;

public class Person
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    // Navs for one-to-many
    public List<Blog> OwnedBlogs { get; set; }
    public List<Post> AuthoredPosts { get; set; }

    // One-to-one foreign key
    public int? PhotoId { get; set; }
    public PersonPhoto Photo { get; set; }
}
