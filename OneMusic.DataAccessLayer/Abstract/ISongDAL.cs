using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public interface ISongDAL : IGenericDal<Song>
    {
        List<Song> GetSongsWithAlbumAndArtist();

        List<Song> GetSongsWithAlbumByUserId(int id);//Kullanıcının ID gore sarkıları alma.

        List<Song> GetSongWithAlbum();
        List<Song> GetSongsByAlbumId(int id);
    }
}
