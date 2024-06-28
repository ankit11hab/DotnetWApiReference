namespace Blog.Api;

public record struct BlogSummaryResponse
(
    int Id,
    string Url,
    int Rating,
    PersonSummaryResponse Owner
);
