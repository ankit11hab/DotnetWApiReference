namespace Blog.Api;

public static class PostMapper
{
    // CreateReq -> Post
    public static Post toPostFromCreateRequest(this CreatePostRequest req) {
        return new Post() {
            Title = req.Title,
            Content = req.Content,
            BlogId = req.BlogId,
            AuthorId = req.AuthorId,
            Tags = new List<Tag>()
        };
    }

    // Post -> SummaryRes
    public static PostSummaryResponse toPostSummaryResponse(this Post post) {
        return new PostSummaryResponse(
            post.Title,
            post.Author.toPersonSummaryResponse(),
            post.Tags.Select(t => t.toTagSummaryResponse()).ToList()
        );
    }
    
    // Post -> DetailRes
    public static PostDetailResponse toPostDetailResponse(this Post post) {
        return new PostDetailResponse(
            post.Title,
            post.Content,
            post.Blog.toBlogSummaryResponse(),
            post.Author.toPersonSummaryResponse(),
            post.Tags.Select(t => t.toTagSummaryResponse()).ToList()
        );
    }
}
