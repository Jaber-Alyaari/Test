using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApplication8.Models;

namespace WebApplication8.Irepository
{
    public interface IPostRepository
    {
        Post GetPostById(int postId);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int postId);
        IEnumerable<Post> GetAllPost();
    }
}
