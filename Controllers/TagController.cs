using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/tag")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly BloggingContext _dbContext;
    public TagController(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTag([FromRoute] int id) {
        var tag = await _dbContext.Tags
                                .Include(t => t.Posts)
                                .FirstOrDefaultAsync(t => t.Id == id);
        if(tag is null) return NotFound();
        var tagRes = tag.toTagSummaryResponse();
        return Ok(tagRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest req) {
        var tag = req.toTagFromCreateRequest();
        await _dbContext.Tags.AddAsync(tag);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
