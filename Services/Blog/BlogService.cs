
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class BlogService : IBlogService
{
    private readonly BloggingContext _dbContext;
    public BlogService(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<List<Blog>> GetAllAsync(BlogQuery query)
    {
        var blogsQ = _dbContext.Blogs
                                .Include(b => b.Owner)
                                .AsQueryable();
        blogsQ = blogsQ.Where(b => b.Rating >= query.MinRating);
        if(!string.IsNullOrEmpty(query.Url)) {
            blogsQ = blogsQ.Where(b => b.Url != null && b.Url.Contains(query.Url));
        }
        int skip = (query.PageNumber - 1)*query.PageSize;
        var blogs = await blogsQ.Skip(skip).Take(query.PageSize).ToListAsync();
        return blogs;
    }

    public async Task<Blog?> GetByIdAsync(int id) 
    {
        var blog = await _dbContext.Blogs
                                .Include(b => b.Owner)
                                .Include(b => b.Posts)
                                .ThenInclude(p => p.Author)
                                .Include(b => b.Posts)
                                .ThenInclude(p => p.Tags)
                                .FirstOrDefaultAsync(b => b.Id == id);
        return blog;
    }
    
    public async Task SaveAsync() {
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(Blog blog)
    {
        await _dbContext.Blogs.AddAsync(blog);
        await SaveAsync();
    }


    public async Task DeleteAsync(Blog blog) {
        _dbContext.Blogs.Remove(blog);
        await SaveAsync();
    }
}
