using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicodes.Models.Blog.BlogData
{
    //======================================================================
    //
    //        Copyright (C) 2014-2016 Magicodes团队    
    //        All rights reserved
    //        description :提交上下文
    //        created by Anton at 2014年11月6日 2014年11月7日 20:35:07
    //        http://www.magicodes.net
    //
    //======================================================================
    /// <summary>
    /// 提交上下文
    /// </summary>
    public class DbCommit
    {
        public static int Commit()
        {
            try
            {
               return EFContextFactory.GetCurrentDbContext().SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ExcuteSql(string strSql, DbParameter[] parameters)
        {
            try
            {
                var dbContext = EFContextFactory.GetCurrentDbContext();
                return parameters == null ? dbContext.Database.ExecuteSqlCommand(strSql)
                     : EFContextFactory.GetCurrentDbContext().Database.ExecuteSqlCommand(strSql, parameters);


            }
            catch (Exception ex)
            {
                throw new Exception("执行SQL语句时发生异常:" + ex.Message);
            }
        }
    }
}
