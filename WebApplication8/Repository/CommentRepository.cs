using WebApplication8.Data;
using WebApplication8.Irepository;
using WebApplication8.Models;

namespace WebApplication8.Repository
{
    public class CommentRepository:ICommentRepository
    {
        private readonly AppDbContext _dbContext;

        public CommentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Comment GetCommentById(int CommentId)
        {
            return _dbContext.Comment.FirstOrDefault(p => p.Id == CommentId);
        }

        public void AddComment(Comment Comment)
        {
            _dbContext.Comment.Add(Comment);
            _dbContext.SaveChanges();
        }

        public void UpdateComment(Comment Comment)
        {
            _dbContext.Comment.Update(Comment);
            _dbContext.SaveChanges();
        }

        public void DeleteComment(int CommentId)
        {
            var Comment = _dbContext.Comment.FirstOrDefault(p => p.Id == CommentId);
            if (Comment != null)
            {
                _dbContext.Comment.Remove(Comment);
                _dbContext.SaveChanges();
            }
        }
    }
}
