using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :IUnitOfWork
//        description :
//
//        created by 雪雁 at  2014/12/23 23:26:31
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Commit();
        void Rollback();
    }
}
