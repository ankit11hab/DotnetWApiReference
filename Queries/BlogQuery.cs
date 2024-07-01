namespace Blog.Api;

public class BlogQuery
{
    public string? Url { get; set; }
    public int MinRating { get; set; } = 0;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
