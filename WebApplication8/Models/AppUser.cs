﻿using Microsoft.AspNetCore.Identity;

namespace WebApplication8.Models
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }
        public ICollection<Post>? posts { get; set; }
    }
}
