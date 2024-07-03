using System.ComponentModel.DataAnnotations;

namespace Blog.Api;

public class Role
{
    [Key]
    public string Id { get; set; } = null!;
    public List<Person> Persons { get; set; } = new List<Person>();
}
