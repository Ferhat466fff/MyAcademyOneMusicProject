using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{//Rol atama
	[Authorize(Roles = "Admin")]//Sadece Admin Rolü erişebilsin.
	public class RoleAssingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleAssingController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Kullanıcı Listeleme
        public IActionResult Index()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        //Çoka Çok tablo Kullanıcı var Kullanıcının rolü var 
        [HttpGet]
        public async Task<IActionResult> AssingRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["userid"] = user.Id;
            var roles = _roleManager.Roles.ToList();//Rolleri Listeliyoruz

            var userRoles = await _userManager.GetRolesAsync(user);//Kullancıının Rolu var mı yok muona bakacak

            List<RoleAssignViewModel> roleAssignList = new List<RoleAssignViewModel>();//Seçtiğimiz kullanıcının rolleri listelmee

            foreach (var item in roles)
            {
                var model = new RoleAssignViewModel();
                model.RoleId = item.Id;
                model.RoleName = item.Name;
                model.RoleExist = userRoles.Contains(item.Name);

                roleAssignList.Add(model);



            }
            return View(roleAssignList);
        }

        [HttpPost]
        public async Task<IActionResult> AssingRole(List<RoleAssignViewModel> model)
        {
            int userid = (int)TempData["userid"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);

            foreach (var item in model)//Rolleri atama seçtiğimiz rollere bakıyor if ise ekliyor değil ise rolü eklemiyor.
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }

                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("Index");
        }
       
    }
}
