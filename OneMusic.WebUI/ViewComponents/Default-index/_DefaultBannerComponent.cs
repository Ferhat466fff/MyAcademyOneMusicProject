using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_index
{
   
    public class _DefaultBannerComponent:ViewComponent
    {
        private readonly IBannerService _bannerService;

        public _DefaultBannerComponent(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        //Banner(afiş) Listeleme
        public IViewComponentResult Invoke()
        {
            var values = _bannerService.TGetlist();
            return View(values);
        }
    }
}
