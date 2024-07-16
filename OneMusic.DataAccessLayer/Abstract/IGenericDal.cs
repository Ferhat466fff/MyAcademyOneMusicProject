using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{   //1.Adım-->Abstract içerisinde IGenericDAL-->Crud İşlemlerini Tanımlıyoruz.
    //2.Adım-->Repositories İçerisine GenericRepository İşlemlerini Yaptıracağız.
    //3.Adım-->Abstract içerisine Sıınıflarımızı İlişkilendiriyoruz.
    public interface IGenericDal <T>where T:class //Generic Oluşturduk.T türü Class Olacak.
    {   //Crud İşlemleri Yapacağız.
        List<T> Getlist();//Listeleme

        T GetById(int id);//Id göre getirme.
        void Create(T entity);//Oluşturma.
        void Update(T entity);//Güncelleme
        void Delete(int id);//Sil







    }
}
