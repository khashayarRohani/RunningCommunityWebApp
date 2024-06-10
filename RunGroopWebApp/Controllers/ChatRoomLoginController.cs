using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ChatRoomLoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public ChatRoomLoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult ChatLogin()
        {
            var userDetail = new ChatUserDetailViewModel();

            return View(userDetail);
        }

        [HttpPost]
        public async Task<IActionResult> ChatLogin(ChatUserDetailViewModel userDetail)
        {
            if (!ModelState.IsValid)
            {
                return View(userDetail);
            }
            var user = await _userManager.FindByEmailAsync(userDetail.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userDetail.Password);
                if (passwordCheck)
                {
                    return RedirectToAction("ChatRoom", "ChatRoomLogin", new { username = userDetail.EmailAddress });

                }
                return View(userDetail);
            }
            else
                return RedirectToAction("ChatLogin");
        }

        public IActionResult ChatRoom(string username)
        {
            ViewData["username"] = username;
            return View();
        }
    }
}
