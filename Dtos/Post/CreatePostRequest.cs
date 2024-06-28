namespace Blog.Api;

public record class CreatePostRequest
(
    string Title,
    string Content,
    int BlogId,
    int AuthorId,
    List<int> Tags
);
