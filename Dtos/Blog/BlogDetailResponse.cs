namespace Blog.Api;

public record class BlogDetailResponse
(
    int Id,
    string Url,
    string Rating,
    PersonSummaryResponse Owner
);