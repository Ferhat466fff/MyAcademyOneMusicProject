using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{
	[AllowAnonymous]
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Signup()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Signup(RegisterViewModel Model)//OneMusic.WebUI Model Klasörüne RegisterViewModel Oluşturduk(kullanacagımız değerleri giridk.)
		{ // async-->Birden fazla işlemin bir arada yapılması.
			AppUser user = new AppUser //Appuser User(entitiy layer sınıf)
			{
				Email = Model.Email,
				UserName = Model.UserName,
				Name = Model.Name,
				Surname = Model.SurnName
			};

			if (Model.Password == Model.ConfirmPassword)//ŞifreLER EŞLEŞİYORSA 
			{
				var result = await _userManager.CreateAsync(user, Model.Password);//async yapı olduğu için -->await eklemen gerekli
				if (result.Succeeded)//İşlem Başarılı Olursa.
				{
					await _userManager.AddToRoleAsync(user, "Visitor");//yeni kaydolan kişi visitor olarak kaydoldu.
					return RedirectToAction("Index", "Login");
				}
				foreach (var item in result.Errors)//hataların acıklamalarını gosterme
				{
					ModelState.AddModelError("", item.Description);
				}
			}

			return View();
		}

	}
}
