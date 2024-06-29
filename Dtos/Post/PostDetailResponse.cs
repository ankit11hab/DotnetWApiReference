namespace Blog.Api;

public record class PostDetailResponse
(
    string Title,
    string Content,
    BlogSummaryResponse Blog,
    PersonSummaryResponse Author,
    List<TagSummaryResponse> Tags
);
