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
    public static class EntityHelper
    {
        public static EntityType GetEntityType<T>(this DbContext context) where T : class
        {
            var oc = ((IObjectContextAdapter)context).ObjectContext;
            var entityType = oc.MetadataWorkspace
                               .GetItems(DataSpace.OSpace).OfType<EntityType>()
                               .FirstOrDefault(et => et.Name == typeof(T).Name);
            return entityType;
        }
        public static string GetEntitySetName<T>(this DbContext context) where T : class
        {
            string className = typeof(T).Name;
            var oc = ((IObjectContextAdapter)context).ObjectContext;
            var container = oc.MetadataWorkspace.GetEntityContainer(oc.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == className
                                    select meta.Name).First();

            return entitySetName;
        }
        public static IEnumerable<string> GetKeyNames<T>(this DbContext context) where T : class
        {
            var entityType = GetEntityType<T>(context);
            return entityType != null
               ? entityType.KeyMembers.Select(k => k.Name).ToArray()
               : Enumerable.Empty<string>();
        }
        public static IEnumerable<NavigationProperty> GetNavigationProperies<T>(this DbContext context)
            where T : class
        {
            var entityType = GetEntityType<T>(context);
            return entityType != null
                ? entityType.NavigationProperties
                : Enumerable.Empty<NavigationProperty>();
        }
        public static IEnumerable<EdmProperty> GetProperies<T>(this DbContext context)
           where T : class
        {
            var entityType = GetEntityType<T>(context);
            return entityType != null
                ? entityType.Properties
                : Enumerable.Empty<EdmProperty>();
        }

        public static IEnumerable<Type> GetKeysTypes<T>(this DbContext context) where T : class
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
