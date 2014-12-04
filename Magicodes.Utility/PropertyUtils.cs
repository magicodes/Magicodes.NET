using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Magicodes.Utility
{
    public class PropertyUtils
    {
        /// <summary>
        /// 创建实体类
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="propertyExpression">实体表达式</param>
        /// <returns></returns>
        public static TEntity CreateEntity<TEntity>(Expression<Func<TEntity, TEntity>> propertyExpression) where TEntity : new()
        {
            TEntity entity = new TEntity();
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            var memberInitExpression = propertyExpression.Body as MemberInitExpression;
            if (memberInitExpression == null)
                throw new ArgumentException("The update expression must be of type MemberInitExpression.",
                    "propertyExpression");
            foreach (MemberBinding binding in memberInitExpression.Bindings)
            {
                //属性字段
                string propertyName = binding.Member.Name;
                //属性值
                object propertyValue;
                var memberAssignment = binding as MemberAssignment;
                if (memberAssignment == null)
                    throw new ArgumentException(
                        "The update expression MemberBinding must only by type MemberAssignment.", "propertyExpression");

                Expression memberExpression = memberAssignment.Expression;
                if (memberExpression.NodeType == ExpressionType.Constant)
                {
                    var constantExpression = memberExpression as ConstantExpression;
                    if (constantExpression == null)
                        throw new ArgumentException(
                            "The MemberAssignment expression is not a ConstantExpression.", "propertyExpression");

                    propertyValue = constantExpression.Value;
                }
                else
                {
                    LambdaExpression lambda = Expression.Lambda(memberExpression, null);
                    propertyValue = lambda.Compile().DynamicInvoke();
                }
                foreach (var propertie in properties.Where(propertie => propertie.Name == propertyName))
                {
                    propertie.SetValue(entity, propertyValue, null);
                    break;
                }
            }
            return entity;
        }
    }
}
