using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);

        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();//getting userId without checking the database(check ClaimsPrincipalExtensions)
            var createClubViewModel = new CreateClubViewModel
            {
                AppUserId = curUserId
            };
            return View(createClubViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var res = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club()
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = res.Url.ToString(),
                    AppUserId=clubVM.AppUserId,
                    Address = new Address()
                    {
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");

            }
            else { ModelState.AddModelError("", "Photo Uploading is Failed"); }
            return View(clubVM);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);

            if (club == null)
            {
                return View("Error");
            }

            var clubVM = new EditClubViewModel
            {

                Title = club.Title,
                Description = club.Description,
                URL = club.Image,
                Address = club.Address,
                AddressId = club.AddressId,
                ClubCategory = club.ClubCategory,
            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit");
                return View("Edit", clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Picture deletion is failed");
                    return View("Edit", clubVM);
                }
                var resultPhoto = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = resultPhoto.Url.ToString(),
                    Address = clubVM.Address,
                    AddressId = clubVM.AddressId
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);

            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null)
            {
                return View("Error");
            }
            return View(clubDetails);
        }
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null)
            {
                return View("Error");
            }
            _clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }


    }
}
