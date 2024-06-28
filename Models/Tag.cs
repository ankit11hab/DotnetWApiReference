namespace Blog.Api;

public class Tag
{
    public int Id { get; set; }
    public string Title { get; set; }

    // Many-to-many
    public List<PostTag> Posts { get; set; }
}
