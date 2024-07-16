using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.ConCreate;
using OneMusic.DataAccessLayer.Migrations;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.ConCreate
{
    public class SongManager : ISongService
    {
        private readonly ISongDAL _songDAL;

        public SongManager(ISongDAL songDal)
        {
            _songDAL = songDal;
        }

        public void TCreate(Song entity)
        {
            _songDAL.Create(entity);
        }

        public void TDelete(int id)
        {
            _songDAL.Delete(id);
        }

        public Song TGetById(int id)
        {
            return _songDAL.GetById(id);

        }

        public List<Song> TGetlist()
        {
            return _songDAL.Getlist();
        }



        public List<Song> TGetSongsByAlbumId(int id)
        {
            return _songDAL.GetSongsByAlbumId(id);
        }
        public List<Song> TGetSongWithAlbum()
        {
            return _songDAL.GetSongWithAlbum();
        }





        public List<Song> TGetSongsWithAlbumAndArtist()
        {
            return _songDAL.GetSongsWithAlbumAndArtist();
        }
        public List<Song> TGetSongsWithAlbumByUserId(int id)
        {
            return _songDAL.GetSongsWithAlbumByUserId(id);
        }

        

        public void TUpdate(Song entity)
        {
            _songDAL.Update(entity);
        }
    }
}
