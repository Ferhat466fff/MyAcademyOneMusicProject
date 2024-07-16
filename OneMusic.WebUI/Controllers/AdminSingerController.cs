using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.validators;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
	[Authorize(Roles = "Admin")]//Sadece Admin Rolü erişebilsin.
	public class AdminSingerController : Controller
    {
        private readonly ISingerService _singerService;

         public AdminSingerController(ISingerService singerService)
        {
            _singerService = singerService;
        }

        public IActionResult Index()
        {
            var values = _singerService.TGetlist();
            return View(values);
        }

        public IActionResult DeleteSinger(int id)
        {
            _singerService.TDelete(id);
            return RedirectToAction("Index");
        }
        //Sanatçı Ekleme
        [HttpGet]
        public IActionResult AddSinger()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSinger(Singer singer)
        {
            var validator = new SingerValidator();//Hata Mesajı Verecek.BunsniessLayer validator klasörü oluşturup işlem yaptık.
            ModelState.Clear();
            var result = validator.Validate(singer);
            if(result.IsValid)//Hata yoksa
            {
                _singerService.TCreate(singer);
                return RedirectToAction("Index");
            }
            result.Errors.ForEach(x =>
            {
                ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
            });
            return View();
           
        }

        //Sanatçı Listeleme
        [HttpGet]
        public IActionResult UpdateSinger(int id)
        {
            var values = _singerService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateSinger(Singer singer)
        {
            _singerService.TUpdate(singer);
            return RedirectToAction("Index");
        }





    }
}
