namespace Blog.Api;

public static class BlogMapper
{
    // CreateReq -> Blog
    public static Blog toBlogFromCreateRequest(this CreateBlogRequest req) {
        return new Blog() {
            Url = req.Url,
            Rating = req.Rating,
            OwnerId = req.OwnerId
        };
    }

    // Blog -> SummaryRes
    public static BlogSummaryResponse toBlogSummaryResponse(this Blog blog) {
        return new BlogSummaryResponse(
            blog.Id,
            blog.Url!,
            blog.Rating,
            blog.Owner.toPersonSummaryResponse()
        );
    }

    // Blog -> DetailRes
    public static BlogDetailResponse toBlogDetailResponse(this Blog blog) {
        return new BlogDetailResponse(
            blog.Id,
            blog.Url!,
            blog.Rating,
            blog.Owner.toPersonSummaryResponse(),
            blog.Posts.Select(p => p.toPostSummaryResponse()).ToList()
        );
    }
}
