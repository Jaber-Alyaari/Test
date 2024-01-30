using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApplication8.Data;
using WebApplication8.Irepository;
using WebApplication8.Models;

namespace WebApplication8.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post GetPostById(int postId)
        {
            return _dbContext.Post.FirstOrDefault(p => p.Id == postId);
        }

        public IEnumerable<Post> GetAllPost()
        {
            return _dbContext.Post.Where(p=> p.IsActive==true).ToList();
        }

        public void AddPost(Post post)
        {
            _dbContext.Post.Add(post);
            _dbContext.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _dbContext.Post.Update(post);
            _dbContext.SaveChanges();
        }
       
        public void DeletePost(int postId)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                _dbContext.Post.Remove(post);
                _dbContext.SaveChanges();
            }
        }
    }
}
