using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/blog")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly BloggingContext _dbContext;
    public BlogController(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogs() {
        var blogs = await _dbContext.Blogs
                            .Include(b => b.Owner)
                            .ToListAsync();
        var blogsRes = blogs.Select(b => b.toBlogSummaryResponse());
        return Ok(blogsRes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog([FromRoute] int id) {
        var blog = await _dbContext.Blogs
                                .Include(b => b.Owner)
                                .Include(b => b.Posts)
                                .ThenInclude(p => p.Author)
                                .Include(b => b.Posts)
                                .ThenInclude(p => p.Tags)
                                .FirstOrDefaultAsync(b => b.Id == id);
        if(blog is null) return NotFound();
        var blogRes = blog.toBlogDetailResponse();
        return Ok(blogRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest req) {
        var blog = req.toBlogFromCreateRequest();
        await _dbContext.Blogs.AddAsync(blog);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog([FromRoute] int id, [FromBody] UpdateBlogRequest req) {
        var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        if(blog is null) return NotFound();
        req.toBlogFromUpdateRequest(blog);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog([FromRoute] int id) {
        var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        if(blog is null) return NotFound();
        _dbContext.Blogs.Remove(blog);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
