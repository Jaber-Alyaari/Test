namespace WebApplication8.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        //public int UserId { get; set; }
        //public AppUser User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
