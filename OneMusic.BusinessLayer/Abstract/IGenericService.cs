﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {   //DataAccessLayer Dal ile hepsi aynı Başına T koyduk karışmasın diye.
        List<T> TGetlist();

        T TGetById(int id);
        void TCreate(T entity);
        void TUpdate(T entity);
        void TDelete(int id);
    }
}
