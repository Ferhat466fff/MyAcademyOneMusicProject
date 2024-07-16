using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
     public interface IAlbumDal : IGenericDal<Album>
    {
        //Albümleri Şarkılarla getiren method
       //ConCreate Klasöründe GetAlbumsWithSinger şakıları listeliyoruz.
        List<Album> GetAlbumsByArtist(int id);
        List<Album> GetAlbumswithArtist();
    }
}
