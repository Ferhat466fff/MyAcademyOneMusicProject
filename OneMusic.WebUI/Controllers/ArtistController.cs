using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.ConcCrate;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArtistController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISingerService _singerService;
        private readonly IAlbumService _albumService;
        private readonly ISongService _songService;

        public ArtistController(UserManager<AppUser> userManager, ISingerService singerService, IAlbumService albumService, ISongService songService)
        {
            _userManager = userManager;
            _singerService = singerService;
            _albumService = albumService;
            _songService = songService;
        }

        //Artist Listeleme
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.GetUsersInRoleAsync("Artist");//Rolü Artist olan kullanıcıları Listeleme.
            return View(values);
        }
        //artist Silme(hata var )
        public IActionResult DeleteArtist(int id)
        {
            _songService.TDelete(id);
            return RedirectToAction("Index");
        }
    }
}
