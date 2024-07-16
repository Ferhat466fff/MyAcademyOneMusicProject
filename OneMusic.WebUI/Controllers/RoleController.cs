using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
	[Authorize(Roles = "Admin")]//Sadece Admin Rolü erişebilsin.
	public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //rol listeleme
        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        //Rol Ekleme
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AppRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        //Silme

        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(value);
            return RedirectToAction("Index");
        }

        //Güncelleme
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(AppRole role)
        {
            await _roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

    }
}
