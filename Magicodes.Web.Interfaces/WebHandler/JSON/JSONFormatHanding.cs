using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler.JSON
{
    public enum JSONFormatHanding
    {
        /// <summary>
        /// "\/Date(1356044400000+0100)\/"
        /// </summary>
        DateFormatHandling_MicrosoftDateFormat,
        /// <summary>
        /// 标准时间
        /// </summary>
        DateFormatHandling_IsoDateFormat,
        /// <summary>
        /// 忽视默认值的属性
        /// </summary>
        DefaultValueHandling_Ignore,
        /// <summary>
        /// 忽视为NULL的属性
        /// </summary>
        NullValueHandling_Ignore,
        /// <summary>
        /// 输出类型名称
        /// </summary>
        TypeNameHandling_All,
        /// <summary>
        /// yy-MM-dd HH:mm:ss
        /// </summary>
        DateFormatString_yyMMddHHmmss,
        /// <summary>
        /// yy--MM-dd HH:mm:ss
        /// </summary>
        DateFormatString_yyyyMMddHHmmss
    }
}
