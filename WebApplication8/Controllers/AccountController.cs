using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication8.Data;
using WebApplication8.Models;
using WebApplication8.View_Model;

namespace WebApplication8.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;


        public AccountController(SignInManager<AppUser> signInManager,
           UserManager<AppUser> userManager,
           AppDbContext context
           )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _context = context;
          
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(loginVM);
                }
                else
                {
                    var result = await signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password,loginVM.RememberMe, true);

                    if (result.Succeeded)
                    {
                        var user = await userManager.FindByEmailAsync(loginVM.Email);
                        bool isUser = await userManager.IsInRoleAsync(user, "User");
                        if (isUser)
                        {
                            return RedirectToAction("Index", "Post");
                        }
                        else { return RedirectToAction("Index", "Post"); }
                    }

                    else
                    {

                        ModelState.AddModelError(string.Empty, "عفوا, كلمة السر أو البريد الإلكتروني غير صحيح");
                        return View(loginVM);
                    }
                }
            }
            catch (Exception)
            {
                return View(loginVM);
            }


        }

        public IActionResult Register()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterVM register)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "يرجى التأكد من تعبئة كل البيانات و بشكل صحيح";
                    return View(register);

                }
                else
                {

                    var found = _context.Users.FirstOrDefault(x => x.Email == register.Email);
                    if (found != null)
                    {
                        TempData["found"] = "المعذرة, هذا الإيميل موجود من قبل , يرجى إختيار إيميل آخر";
                        return View(register);
                    }
                    var addUser = new AppUser
                    {

                        UserName = register.Email,
                        Email = register.Email,
                        Name=register.Name
                    };

                 
                    var result = await userManager.CreateAsync(addUser, register.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(addUser, "Admin");
                        return RedirectToAction("Login", "Account");

                    }
                    else
                    {
                        TempData["Error"] = "المعذرة, حصل خطأ ما في إنشاء حساب, الرجاء المحاولة لاحقا";
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(register);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Error"] = "حدث خطأ غير متوقع , الرجاء المحاولة لاحقا";
                return View(register);
            }




        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAccount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Register", "Account");
            }

            // If the deletion fails, display an error message to the user
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            return View(user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(AppUser updatedUser, string role)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;

                // Update other user properties as needed

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Update user role
                    var currentRole = await userManager.IsInRoleAsync(user, "User") ? "User" : "Admin";
                    if (currentRole != role)
                    {
                        await userManager.RemoveFromRoleAsync(user, currentRole);
                        await userManager.AddToRoleAsync(user, role);
                    }

                    return Json(new { success = true });
                }

                // If the update fails, return an error message
                var errors = result.Errors.Select(e => e.Description);
                return Json(new { success = false, errors });
            }
            else
            {
                return   NotFound();
            }
        }



        }
    }
