using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System.Runtime.InteropServices;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]//Artist areasında çalıştığımızı gösteriyoruz
    [Authorize(Roles = "Artist")] // Rolu Artist olan kişiler buraya erişebilsin
    [Route("[area]/[controller]/[action]/{id?}")] // Alışma mantığını gösterdik burada.
    public class MyAlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<AppUser> _userManager;

        public MyAlbumController(IAlbumService albumService, UserManager<AppUser> userManager)
        {
            _albumService = albumService;
            _userManager = userManager;
        }
        //Albüm Listeleme
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            var values = _albumService.TGetAlbumsByArtist(userId); // TGetAlbumsByArtist data access ve business layer abstract concrete oluşturuyoruz.
            return View(values);
        }

        //Yeni Albüm Ekleme
        [HttpGet]
        public IActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(Album album)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            album.AppUserId = user.Id;
            _albumService.TCreate(album);
            return RedirectToAction("Index");
        }

        //Albüm Silme
        public IActionResult DeleteAlbum(int id)
        {
            _albumService.TDelete(id);
            return RedirectToAction("Index");
        }

        //Albüm Güncelleme
        [HttpGet]
        public IActionResult UpdateAlbum(int id)
        {
            var values = _albumService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAlbum(Album album)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            album.AppUserId = user.Id;
            _albumService.TUpdate(album);
            return RedirectToAction("Index");
        }
    }
}
