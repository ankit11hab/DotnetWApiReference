namespace Blog.Api;

public record class UpdateBlogRequest
(
    string Url,
    int Rating,
    int OwnerId
);
