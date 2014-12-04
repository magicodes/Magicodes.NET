using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Utility
{
    public static class ObjectUtils
    {
        /// <summary>
        /// 判断是否为NULL
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNull(this object instance)
        {
            return instance == null;
        }
        /// <summary>
        /// 判断不为NULL
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object instance)
        {
            return (!instance.IsNull());
        }
    }
}
