using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;

		public AdminLayoutController(UserManager<AppUser> usermanager)
		{
			_usermanager = usermanager;
		}
        
    }
}
