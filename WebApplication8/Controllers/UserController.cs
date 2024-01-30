using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication8.Models;
using System.Configuration;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebApplication8.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var usersWithRecentPosts = GetUsersWithRecentPosts();

            // Do something with the list of users

            return View(usersWithRecentPosts);
        }
        private IEnumerable<AppUser> GetUsersWithRecentPosts()
        {
            string connectionString = _configuration.GetConnectionString("ApplicationDBContextConnection");

            string query = "SELECT DISTINCT u.* FROM AspNetUsers u INNER JOIN Post p ON u.Id = p.AppUserId WHERE p.CreatedAt >= @StartDate";

            DateTime startDate = DateTime.Now.AddDays(-30);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var users = connection.Query<AppUser>(query, new { StartDate = startDate });

                return users;
            }
        }

    }

}
