namespace Blog.Api;

public interface IPostService
{
    Task<Post?> GetByIdAsync(int id);
    Task SaveAsync();
    Task CreateAsync(Post post);
    Task DeleteAsync(Post post);
    Task<List<Tag>> GetTagsAsync(List<int> tagList);
}
