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
    public class SingerManager : ISingerService
    {
        private readonly ISingerDal _singerdal;
        public SingerManager(ISingerDal singerdal)
        {
            _singerdal = singerdal;
        }
        //Şarkıcı ekleme
        public void TCreate(Singer entity)
        {
            _singerdal.Create(entity);
        }
        //Şarkıcı sil
        public void TDelete(int id)
        {
            _singerdal.Delete(id);
        }
        //Şarkıcı ıd gore geyir
        public Singer TGetById(int id)
        {
            return _singerdal.GetById(id);
        }
        //Şarkıcı listele
        public List<Singer> TGetlist()
        {
            return _singerdal.Getlist();
        }
        //Şarkıcı Güncelle
        public void TUpdate(Singer entity)
        {
            _singerdal.Update(entity);
        }
    }
}
