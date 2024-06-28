namespace Blog.Api;

public record class PostDetailResponse
(
    string Title,
    string Content,
    int BlogId,
    int AuthorId,
    List<int> Tags
);
