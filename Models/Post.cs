using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Api;

public class Post
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; } = null!;

    // Shadow + Nav property for one-to-many
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;

    // One-to-many
    public int AuthorId { get; set; }
    public Person Author { get; set; } = null!;

    // Many-to-many
    public List<Tag> Tags { get; set; } = new List<Tag>();
}
