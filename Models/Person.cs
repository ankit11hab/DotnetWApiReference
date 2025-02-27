﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Api;

public class Person
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(250)]
    public string Password { get; set; } = null!;
    
    public List<Role> Roles { get; set; } = new List<Role>();

    // Navs for one-to-many
    public List<Blog> OwnedBlogs { get; set; } = new List<Blog>();
    public List<Post> AuthoredPosts { get; set; } = new List<Post>();

    // One-to-one foreign key
    public int? PhotoId { get; set; }
    public PersonPhoto? Photo { get; set; }
}
