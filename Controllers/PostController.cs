using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly BloggingContext _dbContext;
    public PostController(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost([FromRoute] int id) {
        var post = await _dbContext.Posts
                                .Include(p => p.Blog)
                                .ThenInclude(b => b.Owner)
                                .Include(p => p.Author)
                                .Include(p => p.Tags)
                                .FirstOrDefaultAsync(p => p.Id == id);
        if(post is null) return NotFound();
        var postRes = post.toPostDetailResponse();
        return Ok(postRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest req) {
        var post = req.toPostFromCreateRequest();
        post.Tags = await _dbContext.Tags.Where(t => req.Tags.Contains(t.Id)).ToListAsync();
        await _dbContext.Posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostRequest req) {
        var post = await _dbContext.Posts
                                    .Include(p => p.Tags)
                                    .FirstOrDefaultAsync(p => p.Id == id);
        if(post is null) return NotFound();
        req.toPostFromUpdateRequest(post);
        post.Tags = await _dbContext.Tags.Where(t => req.Tags.Contains(t.Id)).ToListAsync();
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id) {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if(post is null) return NotFound();
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
