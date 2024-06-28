namespace Blog.Api;

public record class BlogDetailResponse
(
    int Id,
    string Url,
    int Rating,
    PersonSummaryResponse Owner,
    List<PostSummaryResponse> Posts
);