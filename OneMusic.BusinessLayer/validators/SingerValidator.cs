using FluentValidation;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.validators
{
    public class SingerValidator :AbstractValidator<Singer>
    {
        public SingerValidator()//Hata Mesajı Verme
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sanatçı Adı Boş Bırakılamaz").MaximumLength(50).WithMessage("En Fazla 50 Karakter Girebilirsiniz.");
            //Kural(name).boşgeçilemez.mesaj("").MaxUzunluk(15 Karakter)....
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Resim Url Değeri Boş Bırakılamaz");//Üstekiyle Aynı
        }
    }
}
