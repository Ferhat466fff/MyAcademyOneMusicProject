using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]//default UI kısmı burda kullanıcı giriş yapmasına gerek yok.
    public class DefaultController : Controller
    {
        private readonly IMessageService _messageService;

        public DefaultController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Component kullanıyoruz artık partialview kullanmadık. örnek aşada
        //viewcomponent Default-index dosyasının _DefaultBannerComponent açtık.:ViewComponent miras yolunu verdik.
        //Shared-->Component-->_DefaultBannerComponent içine default razor view ekledik.
        //buraya banner ksımımızı aldık(UILayout aldık) default controllerada @await Component.InvokeAsync("_DefaultBannerComponent") metodo yazdık bitti.


        [HttpPost]//DefaultMessageComponent içerisindeki Default methodu için yazdık burayı.
        public IActionResult SendMessage(Message message)
        {
            _messageService.TCreate(message);
            return NoContent();//_DefaultMessageComponent default içerisine swith-alert(mesaj) kısmını yaptık o yüzden bir şey yapmasın dedik.
        }
    }
}
