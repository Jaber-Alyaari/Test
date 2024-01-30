using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Irepository;
using WebApplication8.Models;
using WebApplication8.View_Model;

namespace WebApplication8.Controllers
{
    public class PostController :Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext _context;
        public PostController(IPostRepository postRepository, UserManager<AppUser> userManager,
            AppDbContext context)
        {
            _postRepository = postRepository;
            this.userManager = userManager;
            _context=context;
        }

        //public IActionResult Index()
        //{
        //    var posts = _postRepository.GetAllPost();
        //    return View(posts);
        //}


        public IActionResult Index(DateTime? filterDate)
        {
            var posts = _context.Post.Where(p=> p.IsActive==true).AsQueryable();

            if (filterDate.HasValue)
            {
                // Filter posts based on the specified date
                posts = posts.Where(p => p.CreatedAt.Date == filterDate.Value.Date);
            }

            return View(posts.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(PostVM post)
        {
            if (ModelState.IsValid)
            {
                Post post1 = new Post();
                post1.IsActive = true;
                post1.Title=post.Title;
                post1.Content=post.Content;
                post1.CreatedAt = DateTime.Now;
                var user= await userManager.GetUserAsync(User);
                post1.AppUserId = user.Id;
                _postRepository.AddPost(post1);
                return RedirectToAction("Index");
            }

 
            return View(post);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            //var users = _userRepository.GetAllUsers();
            //ViewBag.Users = users;
            PostVM post1 = new PostVM();
            post1.IsActive = post.IsActive;
            post1.Title = post.Title;
            post1.Content = post.Content;
            post1.CreatedAt = post.CreatedAt;
            post1.AppUserId = post.AppUserId;

            return View(post1);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
         [ValidateAntiForgeryToken]
        public IActionResult Edit(PostVM post)
        {
            if (ModelState.IsValid)
            {
                Post post1 = new Post();
                post1.Id = (int)post.Id;
                post1.IsActive = post.IsActive;
                post1.Title = post.Title;
                post1.Content = post.Content;
                post1.CreatedAt = (DateTime)post.CreatedAt;
                post1.AppUserId = post.AppUserId;
                _postRepository.UpdatePost(post1);
             
                return RedirectToAction("Index");
            }

            return View(post);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                _postRepository.DeletePost(id);
                return RedirectToAction("Index");

            }

        }

      



    }
}
