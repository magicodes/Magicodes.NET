using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :EntityKeyHelper
//        description :
//
//        created by 雪雁 at  2015/1/25 18:25:24
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4
{
    public sealed class EntityKeyHelper
    {
        public EntityType GetEntityType<T>(DbContext context) where T : class
        {
            var oc = ((IObjectContextAdapter)context).ObjectContext;
            var entityType = oc.MetadataWorkspace
                               .GetItems(DataSpace.OSpace).OfType<EntityType>()
                               .FirstOrDefault(et => et.Name == typeof(T).Name);
            return entityType;
        }
        public IEnumerable<string> GetKeyNames<T>(DbContext context) where T : class
        {
            var entityType = GetEntityType<T>(context);
            return entityType != null
                ? entityType.KeyMembers.Select(k => k.Name).ToArray()
                : Enumerable.Empty<string>();
        }
        public IEnumerable<NavigationProperty> GetNavigationProperies<T>(DbContext context)
            where T : class
        {
            var entityType = GetEntityType<T>(context);
            return entityType != null
                ? entityType.NavigationProperties
                : Enumerable.Empty<NavigationProperty>();
        }

        public IEnumerable<Type> GetKeysTypes<T>(DbContext context) where T : class
        {
            //var entityType = GetEntityType<T>(context);
            //return entityType != null
            //    ? entityType.KeyMembers.Select(k => k.GetType()).ToArray()
            //    : Enumerable.Empty<Type>();

            var keyNames = GetKeyNames<T>(context);
            Type type = typeof(T);
            return keyNames.Select(p => type.GetProperty(p).PropertyType).ToArray();
        }

    }
}
