using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    public PostController(IPostService postService) {
        _postService = postService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost([FromRoute] int id) {
        var post = await _postService.GetByIdAsync(id);
        if(post is null) return NotFound();
        var postRes = post.toPostDetailResponse();
        return Ok(postRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest req) {
        var post = req.toPostFromCreateRequest();
        post.Tags = await _postService.GetTagsAsync(req.Tags);
        await _postService.SaveAsync();
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostRequest req) {
        var post = await _postService.GetByIdAsync(id);
        if(post is null) return NotFound();
        req.toPostFromUpdateRequest(post);
        post.Tags = await _postService.GetTagsAsync(req.Tags);
        await _postService.SaveAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id) {
        var post = await _postService.GetByIdAsync(id);
        if(post is null) return NotFound();
        await _postService.DeleteAsync(post);
        return Ok();
    }
}
