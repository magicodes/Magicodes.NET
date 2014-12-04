using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Magicodes.Utility;

namespace Magicodes.Models.Blog.BlogData
{
   public static class EFRepository
    {
        //修改2 按需修改
       public static void UpdateEntity<TEntity>(this DbContext db, Expression<Func<TEntity, TEntity>> propertyExpression) where TEntity : class, new()
        {
            try
            {
                var memberInitExpression = propertyExpression.Body as MemberInitExpression;
                var entity = PropertyUtils.CreateEntity(propertyExpression);
                DbEntityEntry<TEntity> entry = db.Entry(entity);
                entry.State = EntityState.Unchanged;
                if (memberInitExpression != null)
                    foreach (var memberInfo in memberInitExpression.Bindings)
                    {
                        string propertyName = memberInfo.Member.Name;
                        entry.Property(propertyName).IsModified = true;
                    }
                db.Configuration.ValidateOnSaveEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Configuration.AutoDetectChangesEnabled = true;
            }

        }
    }
}
