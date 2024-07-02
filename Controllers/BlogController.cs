using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/blog")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    public BlogController(IBlogService blogService) {
        _blogService = blogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogs([FromQuery] BlogQuery query) {
        var blogs = await _blogService.GetAllAsync(query);
        var blogsRes = blogs.Select(b => b.toBlogSummaryResponse());
        return Ok(blogsRes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog([FromRoute] int id) {
        var blog = await _blogService.GetByIdAsync(id);
        if(blog is null) return NotFound();
        var blogRes = blog.toBlogDetailResponse();
        return Ok(blogRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest req) {
        var blog = req.toBlogFromCreateRequest();
        await _blogService.CreateAsync(blog);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog([FromRoute] int id, [FromBody] UpdateBlogRequest req) {
        var blog = await _blogService.GetByIdAsync(id);
        if(blog is null) return NotFound();
        req.toBlogFromUpdateRequest(blog);
        await _blogService.SaveAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog([FromRoute] int id) {
        var blog = await _blogService.GetByIdAsync(id);
        if(blog is null) return NotFound();
        await _blogService.DeleteAsync(blog);
        return Ok();
    }
}
