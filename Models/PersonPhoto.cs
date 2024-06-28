using System.ComponentModel.DataAnnotations;

namespace Blog.Api;

public class PersonPhoto
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Caption { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = null!;

    // One-to-one
    public Person Person { get; set; }
}
