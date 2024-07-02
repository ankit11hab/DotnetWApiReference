namespace Blog.Api;

public interface IBlogService
{
    Task<List<Blog>> GetAllAsync(BlogQuery query);
    Task<Blog?> GetByIdAsync(int id);
    Task SaveAsync();
    Task CreateAsync(Blog blog);
    Task DeleteAsync(Blog blog);
}
