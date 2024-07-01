namespace Blog.Api;

public record class UpdatePostRequest
(
    string Title,
    string Content,
    int BlogId,
    int AuthorId,
    List<int> Tags
);
