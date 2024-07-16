using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Packaging.Signing;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers//Profil Bilgileri sayfası
{
    [Area("Artist")]//Artist areasında çalıştığımızı gösteriyoruz
    [Authorize(Roles = "Artist")] // Rolu Artist olan kişiler buraya erişebilsin
    [Route("[area]/[controller]/[action]/{id?}")] // Alışma mantığını gösterdik burada.
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        //Profil Bilgilerimi getirme.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);//Giriş Yapan Kullanıcının Değerlerini alma

            var model = new ArtistEditViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName
            };
            return View(model);
        }
        //Profil bilgilerimi Güncellem
        [HttpPost]
        public async Task<IActionResult>Index(ArtistEditViewModel model)
        {
            ModelState.Clear();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);//Giriş Yapan Kullanıcının Değerlerini alma

            //resim seçimi
            if (model.ImageFile !=null)//resim seçildiyse
            {

                var kaynak = Directory.GetCurrentDirectory();//projenin dosya yolunu bul
                var uzantı = Path.GetExtension(model.ImageFile.FileName).ToLower();//Dosyanın uzantısı .jpg .pdf gibi.
                if (uzantı != ".jpg" && uzantı != ".jpeg" && uzantı != ".png")
                {
                    // Desteklenmeyen dosya uzantısı hatası
                    ModelState.AddModelError("ImageFile", "Sadece Resim dosyaları kabul edilir.");
                    // Gerekirse, işlemi sonlandırabilirsiniz.
                    return View(model);
                }
                var dosyaismi = Guid.NewGuid() + uzantı;// Benzersiz bir dosya ismi oluştur
                var dosyayolu = kaynak + "/wwwroot/Images/" + dosyaismi;//kaydedeceğimiz dosyanın yolunu verdik
                var stream = new FileStream(dosyayolu, FileMode.Create);//yeni bir dosya yolu oluşturuk eğerki o dosya yolu varsa üzerine yazar.
                await model.ImageFile.CopyToAsync(stream);
                user.ImageUrl = "/images/" + dosyaismi;
            }
          
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await _userManager.CheckPasswordAsync(user, model.OldPassword);//Kullanıcı Mevcut şifresini girdi 
            if(result == true)
            {
                if (model.NewPassword != null && model.ConfirmPassword == model.NewPassword)//Yeni şifre null değilse ve tekrar girilen şifreyle eşleşiyorsa.
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)//işlem başarılı olmazsa
                    {
                        foreach (var item in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            return View();
                        }
                    }
                }
                var updateresult= await _userManager.UpdateAsync(user);
                if(updateresult.Succeeded)//Şifre doğruysa
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            ModelState.AddModelError("", "Mevcut Şifreniz Hatalı");
            return View();
        }

    } 
}
