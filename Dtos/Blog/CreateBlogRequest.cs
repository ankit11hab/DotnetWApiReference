namespace Blog.Api;

public record class CreateBlogRequest
(
    string Url,
    int Rating,
    int OwnerId
);
