﻿using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_index
{
    public class _DefaultMiscellaneousTopComponent : ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultMiscellaneousTopComponent(ISongService songService)
        {
            _songService = songService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _songService.TGetSongWithAlbum().ToList(); // Tüm şarkıları al
            var random = new Random();
            var randomValues = values.OrderBy(x => random.Next()).Take(5).ToList(); // Verileri rastgele sıraya göre düzenle ve ilk 5'ini al
            return View(randomValues);
        }
    }
}
