﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")] // Rolu Artist olan kişiler buraya erişebilsin
    [Route("[area]/[controller]/[action]/{id?}")] // Alışma mantığını gösterdik burada.
    public class MySongController : Controller
    {
        private readonly ISongService _songService;
        private readonly UserManager<AppUser> _userManager;
        private readonly OneMusicContext _oneMusicContext;
        private readonly IAlbumService _albumService;
        public MySongController(ISongService songService, UserManager<AppUser> userManager, IAlbumService albumService, OneMusicContext oneMusicContext)
        {
            _songService = songService;
            _userManager = userManager;
            _albumService = albumService;
            _oneMusicContext = oneMusicContext;
        }
        //Şarkı Listeleme
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _songService.TGetSongsWithAlbumByUserId(user.Id);
            return View(values);
        }
        //Yeni Şarkı Oluştur.
        [HttpGet]
        public IActionResult CreateSong()
        {
            List<SelectListItem> list = (from x in _oneMusicContext.Albums.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AlbumName,
                                             Value = x.AlbumId.ToString()

                                         }).ToList();

            ViewBag.Albums = list;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(ArtistEditSongModel p)
        {
            if (p.SongImageUrl != null && p.SongFile != null)
            {

                var resource2 = Directory.GetCurrentDirectory();
                var extension2 = Path.GetExtension(p.SongImageUrl.FileName);
                var imagename2 = ($"{Guid.NewGuid()}{extension2}");
                var savelocation2 = ($"{resource2}/wwwroot/Images/{imagename2}");
                var stream2 = new FileStream(savelocation2, FileMode.Create);
                await p.SongImageUrl.CopyToAsync(stream2);

                var resource1 = Directory.GetCurrentDirectory();
                var extension1 = Path.GetExtension(p.SongFile.FileName);
                var songname = ($"{Guid.NewGuid()}{extension1}");
                var savelocation1 = ($"{resource1}/wwwroot/Songs/{songname}");
                var stream1 = new FileStream(savelocation1, FileMode.Create);
                await p.SongFile.CopyToAsync(stream1);


                Song son = new Song()
                {
                    SongName = p.SongName,
                    SongImageUrl = imagename2,
                    SongUrl = songname,
                    AlbumId = p.AlbumId

                };

                _songService.TCreate(son);
                return RedirectToAction("Index");

            }

            return View();
        }
        public IActionResult DeleteSong(int id)
        {
            _songService.TDelete(id);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult UpdateSong(int id)
        {
            var values = _songService.TGetById(id);

            List<SelectListItem> list = (from x in _oneMusicContext.Albums.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AlbumName,
                                             Value = x.AlbumId.ToString()
                                         }).ToList();

            ViewBag.Albums = list;

            var model = new ArtistUpdateSongModel()
            {
                Id = values.SongId,
                ImageUrl = values.SongImageUrl,
                SongFileUrl = values.SongUrl,
                SongName = values.SongName,
                AlbumId = values.AlbumId

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSong(ArtistUpdateSongModel p)
        {
            if (p.SongFileUrl != null || p.ImageUrl != null)
            {
                // Mevcut kaydı bul
                var existingSong = _songService.TGetById(p.Id);

                // Eğer mevcut kayıt null değilse, güncelleme işlemini gerçekleştir
                if (existingSong != null)
                {
                    if (p.SongImageUrl != null)
                    {
                        var resource3 = Directory.GetCurrentDirectory();
                        var extension3 = Path.GetExtension(p.SongImageUrl.FileName);
                        var imagename3 = ($"{Guid.NewGuid()}{extension3}");
                        var savelocation3 = ($"{resource3}/wwwroot/Images/{imagename3}");
                        var stream3 = new FileStream(savelocation3, FileMode.Create);
                        await p.SongImageUrl.CopyToAsync(stream3);
                        // Mevcut kaydı güncelle
                        existingSong.SongName = p.SongName;
                        existingSong.SongImageUrl = imagename3;
                        existingSong.AlbumId = p.AlbumId;

                    }
                    else if (p.SongFile != null)
                    {
                        var resource4 = Directory.GetCurrentDirectory();
                        var extension4 = Path.GetExtension(p.SongFile.FileName);
                        var songname4 = ($"{Guid.NewGuid()}{extension4}");
                        var savelocation4 = ($"{resource4}/wwwroot/Songs/{songname4}");
                        var stream4 = new FileStream(savelocation4, FileMode.Create);
                        await p.SongFile.CopyToAsync(stream4);
                        // Mevcut kaydı güncelle
                        existingSong.SongName = p.SongName;
                        existingSong.SongUrl = songname4;
                        existingSong.AlbumId = p.AlbumId;

                    }
                    else if (p.SongImageUrl == null && p.SongImageUrl == null)
                    {
                        // Mevcut kaydı güncelle
                        existingSong.SongName = p.SongName;
                        existingSong.SongImageUrl = p.ImageUrl;
                        existingSong.SongUrl = p.SongFileUrl;
                        existingSong.AlbumId = p.AlbumId;

                    }
                    _songService.TUpdate(existingSong);

                }

                return RedirectToAction("Index");
            }

            return View();

        }










    }
}
