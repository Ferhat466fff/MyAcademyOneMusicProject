using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Controllers
{
	public class ErorrPageController : Controller
	{
		//403 hatası tasarımı
		public IActionResult AccessDenied()
		{
			return View();
		}
		//404 hatası tasarımı
		public IActionResult Error404()
		{
			return View();
		}
	}
}
