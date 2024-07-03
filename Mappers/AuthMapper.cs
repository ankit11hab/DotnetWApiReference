namespace Blog.Api;

public static class AuthMapper
{
    // RegisterReq -> Person
    public static Person toPersonFromRegisterRequest(this RegisterRequest req) {
        return new Person() {
            Name = req.Name,
            Email = req.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
            Roles = new List<Role>()
        };
    }
}
