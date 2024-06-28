using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Api;

public class Blog
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string? Url { get; set; }

    public int Rating { get; set; } = 0;

    // Navigation property for one-to-many
    public List<Post> Posts { get; set; } = new List<Post>();

    // One-to-many
    public int OwnerId { get; set; }
    public Person Owner { get; set; } = null!;
}
