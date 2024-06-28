namespace Blog.Api;

public record class CreateBlogRequest
(
    string Url,
    string Rating,
    int OwnerId
);
