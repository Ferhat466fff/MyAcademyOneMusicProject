﻿using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.ConCreate
{
    public class EFMessageDal : GenericRepository<Message>, IMessageDal
    {
        public EFMessageDal(OneMusicContext context) : base(context)
        {
        }
    }
}
