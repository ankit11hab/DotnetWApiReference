namespace Blog.Api;

public interface IAuthService
{
    Task<Person?> GetPersonByEmail(string email);
    Task<List<Role>> AddUserRole();
    Task AddUser(Person person);
    string GenerateJwtToken(Person person);
}
