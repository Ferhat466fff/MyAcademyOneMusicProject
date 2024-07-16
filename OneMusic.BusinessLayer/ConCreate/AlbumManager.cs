using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.ConCreate;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.ConcCrate
{
    public class AlbumManager : IAlbumService
    {
        private readonly IAlbumDal _albumdal;//DataAccessLayer Albumdal değişken aldık.
		public AlbumManager(IAlbumDal albumdal)
        {
            _albumdal = albumdal;
        }

     

        public void TCreate(Album entity)
        {
            _albumdal.Create(entity);
        }

        public void TDelete(int id)
        {
            _albumdal.Delete(id);
        }

        public List<Album> TGetAlbumsByArtist(int id)
        {
           return _albumdal.GetAlbumsByArtist(id);
        }

        public List<Album> TGetAlbumswithArtist()
        {
            return _albumdal.GetAlbumswithArtist();
        }

        public Album TGetById(int id)
        {
            return _albumdal.GetById(id);
        }

        public List<Album> TGetlist()
        {
            return _albumdal.Getlist();
        }

        public void TUpdate(Album entity)
        {
            _albumdal.Update(entity);
        }
    }
}
