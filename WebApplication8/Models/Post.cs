using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public ICollection<Comment>? comments { get; set; }
    }
}
