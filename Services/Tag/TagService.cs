
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class TagService : ITagService
{
    private readonly BloggingContext _dbContext;
    public TagService(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Tag tag)
    {
        await _dbContext.Tags.AddAsync(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        _dbContext.Tags.Remove(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        var tag = await _dbContext.Tags
                                .Include(t => t.Posts)
                                .FirstOrDefaultAsync(t => t.Id == id);
        return tag;
    }
}
