using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class GoogleRegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public GoogleRegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var properties = _signInManager
               .ConfigureExternalAuthenticationProperties("Google",
               "/GoogleRegister/FromGoogle");
            return new ChallengeResult("Google", properties);

        }
        public async Task<IActionResult> FromGoogle()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var p = info.Principal.Claims.GetEnumerator();
            string firstname = "";
            string lastname = "";
            string username = "";

            for (int i = 0; i < 10; i++)
            {
                p.MoveNext();
                Console.WriteLine(p.Current.Value);
                switch (i)
                {
                    case 2: firstname = p.Current.Value; break;
                    case 3: lastname = p.Current.Value; break;
                    case 4: username = p.Current.Value; break;
                }
            }
            AppUser user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                user = new AppUser()
                {

                    Email = username,
                    UserName = username,
                    EmailConfirmed = true
                };


                await _userManager.CreateAsync(user, "Coding@1234?");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, "Coding@1234?", false, false);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
