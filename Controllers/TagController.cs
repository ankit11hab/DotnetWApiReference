using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/tag")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    public TagController(ITagService tagService) {
        _tagService = tagService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTag([FromRoute] int id) {
        var tag = await _tagService.GetByIdAsync(id);
        if(tag is null) return NotFound();
        var tagRes = tag.toTagSummaryResponse();
        return Ok(tagRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest req) {
        var tag = req.toTagFromCreateRequest();
        await _tagService.CreateAsync(tag);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag([FromRoute] int id)
    {
        var tag = await _tagService.GetByIdAsync(id);
        if(tag is null) return NotFound();
        await _tagService.DeleteAsync(tag);
        return Ok();
    }
}
