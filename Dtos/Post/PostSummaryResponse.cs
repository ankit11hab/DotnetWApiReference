namespace Blog.Api;

public record class PostSummaryResponse
(
    string Title,
    PersonSummaryResponse Author,
    List<TagSummaryResponse> Tags
);
