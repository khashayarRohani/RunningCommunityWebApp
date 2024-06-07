using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;
        public UserController(IUserRepository userRepository, UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _photoService = photoService;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Pace = user.Pace,
                    Mileage = user.Mileage,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl

                };
                result.Add(userViewModel);
            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                Pace = user.Pace,
                Mileage = user.Mileage,
                UserName = user.UserName,
                Image = user.ProfileImageUrl,
           
            };
            return View(userDetailViewModel);
        }
        
    }
}
