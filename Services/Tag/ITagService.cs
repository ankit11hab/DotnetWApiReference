namespace Blog.Api;

public interface ITagService
{
    Task<Tag?> GetByIdAsync(int id);
    Task CreateAsync(Tag tag);
    Task DeleteAsync(Tag tag);
}
