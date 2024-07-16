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
    public class BannerManager : IBannerService
    {
        private readonly IBannerDal _bannerdal;

        public BannerManager(IBannerDal bannerdal)
        {
            _bannerdal = bannerdal;
        }

        public void TCreate(Banner entity)
        {
            _bannerdal.Create(entity);
        }

        public void TDelete(int id)
        {
            _bannerdal.Delete(id);
        }

        public Banner TGetById(int id)
        {
            return _bannerdal.GetById(id);
        }

        public List<Banner> TGetlist()
        {
            return _bannerdal.Getlist();
        }

        public void TUpdate(Banner entity)
        {
            _bannerdal.Update(entity);
        }
    }
}
