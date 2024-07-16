using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_index
{
    
    public class _DefaultAlbumComponent:ViewComponent
    {
        private readonly IAlbumService _albumService;

        public _DefaultAlbumComponent(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        //Albüm Listeleme
        public IViewComponentResult Invoke()
        {
            var values = _albumService.TGetAlbumswithArtist();
            return View(values);
        }
    }
}
