﻿using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.ConCreate
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }
        public void TCreate(Contact t)
        {
            _contactDal.Create(t);
        }

        public void TDelete(int id)
        {
            _contactDal.Delete(id);
        }

        public Contact TGetById(int id)
        {
            return _contactDal.GetById(id);
        }

        

        public List<Contact> TGetlist()
        {
            return _contactDal.Getlist();
        }

        public void TUpdate(Contact t)
        {
            _contactDal.Update(t);
        }
    }
}
