using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.validators
{//Hata Mesajlarını Türkçeleştierme
	public class CustomErrorDescriber :IdentityErrorDescriber
	{

		public override IdentityError PasswordTooShort(int length)//şifrenin kısa olması
		{
			return new IdentityError
			{
				Description = "Şifre en az 6 karakterden oluşmalıdır."
			};
		}
		public override IdentityError PasswordRequiresDigit()//ŞifreGerektiren Rakam
		{
			return new IdentityError
			{
				Description = "Şifre en az 1 rakam(1,9) oluşmalıdır."
			};
		}

		public override IdentityError PasswordRequiresLower()//küçük
		{
			return new IdentityError
			{
				Description = "Şifre en az bir küçük harf(a-z) içermelidir."
			};
		}

		public override IdentityError PasswordRequiresUpper()//büyük
		{
			return new IdentityError
			{
				Description = "Şifre en az bir büyük harf(A,Z) içermelidir."
			};
		}

		public override IdentityError PasswordRequiresNonAlphanumeric()//en az bir sembol içermeli
		{
			return new IdentityError
			{
				Description = "Şifre en az bir özel karakter(*,-,_,+ ...) içermelidir."
			};
		}

        public override IdentityError InvalidUserName(string? userName)
        {
            return new IdentityError
            {
                Description = "Bu Kullanıcı adını kullanamazsınız."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Description = "Kullanıcı adı daha önce alınmıştır."
            };
        }



    }
}
