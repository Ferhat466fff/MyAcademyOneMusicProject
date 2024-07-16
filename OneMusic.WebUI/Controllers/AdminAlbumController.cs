using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
	[Authorize(Roles = "Admin")]//Sadece Admin Rolü erişebilsin.
	public class AdminAlbumController : Controller
    {
        private readonly IAlbumService _albumservice;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISongService _songService;
        private readonly ICategoryService _categoryService;

        public AdminAlbumController(IAlbumService albumservice, UserManager<AppUser> userManager, ISongService songService, ICategoryService categoryService)
        {
            _albumservice = albumservice;
            _userManager = userManager;
            _songService = songService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values = _albumservice.TGetlist();
            return View(values);
        }

        public IActionResult DeleteAlbum(int id)
        {
            _albumservice.TDelete(id);
            return RedirectToAction("Index");
        }

        //Yeni Albüm Eklme
        [HttpGet]
        public async Task<IActionResult> CreateAlbum()
        {
            var categories = _categoryService.TGetlist();//categories kategorilerin listesini aldı.

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();//categori listesine categori name aldı kategorileri seşebileceksin artık.


            ViewBag.CategoryList = categoryList;//categorilerin listesini create albumde kullanmak için viewbag aldı.

           
            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            ViewBag.Singers = artists.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.Name} {a.Surname}"
            }).ToList();//rolü artis olanları viewbeg attı

            return View();
        }

        [HttpPost]
        public IActionResult CreateAlbum(Album album)
        {
         
            _albumservice.TCreate(album);//ekleme methodu
            return RedirectToAction("Index");
        }


        //sanatçı güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateAlbum(int id)
        {
            var categories = _categoryService.TGetlist();//categories kategorilerin listesini aldı.

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName//categorilerin listesini create albumde kullanmak için viewbag aldı.
            }).ToList();


            ViewBag.CategoryList = categoryList;

            var values = _albumservice.TGetById(id);

            // Tüm sanatçıları alıp ViewBag'e ekledim
            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            ViewBag.SingerId = artists.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.Name} {a.Surname}"
            }).ToList();// rolü artis olanları viewbeg attı

            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateAlbum(Album album)
        {
            _albumservice.TUpdate(album);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AlbumsByArtist(int artistId)
        {
            var artist = await _userManager.FindByIdAsync(artistId.ToString());
            var albums = _albumservice.TGetAlbumsByArtist(artistId);

            var model = new AlbumsByArtistViewModel
            {
                Artist = artist,
                Albums = albums,

            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetSongsByAlbumId(int albumId)
        {
            // Albüm ID'sine göre şarkıları getir

            var songs = _songService.TGetSongsByAlbumId(albumId);

            var model = new AlbumsByArtistViewModel
            {
                Songs = songs
            };
            return View(model);
        }




    }

    public class AlbumsByArtistViewModel
    {
        public AppUser Artist { get; set; }
        public Album Album { get; set; } // Albüm bilgisi eklendi

        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
    }





}

