
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class PostService : IPostService
{
    private readonly BloggingContext _dbContext;
    public PostService(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
        await SaveAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _dbContext.Posts.Remove(post);
        await SaveAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        var post = await _dbContext.Posts
                                .Include(p => p.Blog)
                                .ThenInclude(b => b.Owner)
                                .Include(p => p.Author)
                                .Include(p => p.Tags)
                                .FirstOrDefaultAsync(p => p.Id == id);
        return post;
    }

    public async Task<List<Tag>> GetTagsAsync(List<int> tagList)
    {
        var tags = await _dbContext.Tags.Where(t => tagList.Contains(t.Id)).ToListAsync();
        return tags;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
