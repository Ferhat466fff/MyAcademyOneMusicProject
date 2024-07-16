using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_index
{
    public class _DefaultPopularArtistComponent:ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultPopularArtistComponent(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()//şarkııd göre 1 adet ıd getirme.
        {
            var values = _songService.TGetlist().OrderBy(x => x.SongId).Take(1).ToList();
            return View(values);
        }
    }
}
