namespace Blog.Api;

public record class PostSummaryResponse
(
    string Title,
    int BlogId,
    int AuthorId,
    List<int> Tags
);
