namespace Blog.Api;

public record class BlogSummaryResponse
(
    int Id,
    string Url,
    string Rating,
    PersonSummaryResponse Owner
);
