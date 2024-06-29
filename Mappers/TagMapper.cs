namespace Blog.Api;

public static class TagMapper
{
    // CreateReq -> Tag
    public static Tag toTagFromCreateRequest(this CreateTagRequest req) {
        return new Tag() {
            Title = req.Title
        };
    }

    // Tag -> SummaryRes
    public static TagSummaryResponse toTagSummaryResponse(this Tag tag) {
        return new TagSummaryResponse(
            tag.Id,
            tag.Title
        );
    }
}
