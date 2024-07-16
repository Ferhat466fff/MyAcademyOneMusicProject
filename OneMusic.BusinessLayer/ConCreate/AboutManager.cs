using Microsoft.EntityFrameworkCore;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.ConcCrate
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutdal;//DataAccessLayer Aboutdal değişken aldık.

        public AboutManager(IAboutDal aboutdal)
        {
            _aboutdal = aboutdal;
        }

        //Ekleme
        public void TCreate(About entity)
        {
            _aboutdal.Create(entity);
        }
        //Silme
        public void TDelete(int id)
        {
            _aboutdal.Delete(id);
        }
        //Id Göre Getirme
        public About TGetById(int id)
        {
           return _aboutdal.GetById(id);
        }
        //Listeleme
        public List<About> TGetlist()
        {
            return _aboutdal.Getlist();
        }
        //Güncelleme
        public void TUpdate(About entity)
        {
            _aboutdal.Update(entity);
        }
    }
}
