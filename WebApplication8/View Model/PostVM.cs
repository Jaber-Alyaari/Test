using System.ComponentModel.DataAnnotations;
using WebApplication8.Models;

namespace WebApplication8.View_Model
{
    public class PostVM
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(10, ErrorMessage = "Title must be at least 10 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? AppUserId { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        

    }
}