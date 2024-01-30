using WebApplication8.Models;

namespace WebApplication8.Irepository
{
    public interface ICommentRepository
    {
        Comment GetCommentById(int commentId);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int commentId);
    }
}
