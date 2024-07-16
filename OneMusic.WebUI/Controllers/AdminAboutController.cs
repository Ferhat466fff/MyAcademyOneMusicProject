using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]//Sadece Admin Rolü erişebilsin.
    public class AdminAboutController : Controller
    {
        private readonly IAboutService _aboutService;//BUİSNESSLAYER ALIYORUZ.
        private readonly UserManager<AppUser> _usermanager;

        public AdminAboutController(IAboutService aboutService, UserManager<AppUser> usermanager)
        {
            _aboutService = aboutService;
            _usermanager = usermanager;
        }
        //Hakkımda Listeleme
        public async Task<IActionResult> Index()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);//Giriş yapan kişinin ismini ve soyismini getirme.
            TempData["username"] = user.Name + "" + user.Surname;//viewbagle tempdata mantığğı aynı
            var values = _aboutService.TGetlist();
            return View(values);
        }
        //Hakkımda Silme
        public IActionResult DeleteAbout(int id)
        {
            _aboutService.TDelete(id);
            return RedirectToAction("Index");
        }

        //Hakkımda Ekleme
        [HttpGet]
        public IActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAbout(About About)
        {
            _aboutService.TCreate(About);
            return RedirectToAction("Index");
        }

        //Hakkımda Güncelleme
        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var values = _aboutService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About About)
        {
            _aboutService.TUpdate(About);
            return RedirectToAction("Index");
        }
    }
}
