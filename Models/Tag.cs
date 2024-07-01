namespace Blog.Api;

public class Tag
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    // Many-to-many
    public List<Post> Posts { get; set; } = new List<Post>();
}
