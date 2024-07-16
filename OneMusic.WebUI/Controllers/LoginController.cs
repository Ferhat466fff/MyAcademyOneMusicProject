using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers//Giriş Yapma
{
    [AllowAnonymous]//--> Aouhorize dışında bıraktık( login ve registeri)sadece buralara erişeceğiz diğer yerlere kimlik doğrulama yapıp erişilebilir.  programcs işlem yaptık
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signınmanager;
        private readonly UserManager<AppUser> _userManager;

		public LoginController(SignInManager<AppUser> signınmanager, UserManager<AppUser> userManager) 
		{
			_userManager = userManager;
			_signınmanager = signınmanager;

		}

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel Model,string? returnUrl)
        {//async yapı olduğu için -->await eklemen gerekli
            var result = await _signınmanager.PasswordSignInAsync(Model.UserName, Model.Password, false, false);//Kullanıcı Giriş yaptı.
            if (result.Succeeded)//İşlem başarılıysa.
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);//Giriş yapan kullanıcının değerlerini usermanagere atadık.
                var ArtistResult = await _userManager.IsInRoleAsync(user, "Artist");//artist rolüne sahipse 
				var AdminResult = await _userManager.IsInRoleAsync(user, "Admin");//Admin rolüne sahipse
				if (ArtistResult == true)//artistse
                {
                    if(returnUrl !=null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index","MyAlbum", new { area = "Artist" });//artist alanına gonder.
                }
				else if (AdminResult == true)//adminse
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "AdminAbout");//admin alanına gonder.
				}
                else//her ikiside değilse anasayfaya yolla.
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "Default");
				}
            }
            ModelState.AddModelError("", "Kullanıcı Adı Veya Şifre Yanlış");
            return View();
        }


        public async Task<IActionResult>Logout()
        {
            await _signınmanager.SignOutAsync();
            return RedirectToAction("Index", "Default");
                
        }
    }
}
