using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.ViewComponents.Default_index
{
    
    public class _DefaultAlbumSongSaleComponent:ViewComponent
    {
        private readonly IAlbumService _albumService;
        private readonly OneMusicContext _oneMusicContext;
        private readonly ISongService _songService;

        public _DefaultAlbumSongSaleComponent(IAlbumService albumService, OneMusicContext oneMusicContext, ISongService songService)
        {
            _albumService = albumService;
            _oneMusicContext = oneMusicContext;
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _songService.TGetSongsWithAlbumAndArtist().Take(8).ToList();
            return View(values);
        }
    }
}
