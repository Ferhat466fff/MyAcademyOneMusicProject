using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Burada Sınıfların İçerisini Dolduruyoruz.
namespace OneMusic.DataAccessLayer.Repositories
{   //Abstract İçinde GenericDal Yapıyı Oluşturduk ve Aşağıda Miras Aldık O yapıyı Kullanacaz.
    public class GenericRepository<T> : IGenericDal<T> where T : class//IGenericDal ctrl+nokta de altttakilerin oluşturur.
    {
        private readonly OneMusicContext _context;

        public GenericRepository(OneMusicContext context)
        {
            _context = context;
        }
      //Ekleme
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        //Silme
        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(value);
            _context.SaveChanges();
        }
        //Id Göre Getirme
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        //Listeleme
        public List<T> Getlist()
        {
            return _context.Set<T>().ToList();
        }
        //Güncelleme
        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
