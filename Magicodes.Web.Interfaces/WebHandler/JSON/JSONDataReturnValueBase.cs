using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler.JSON
{
    public class JSONDataReturnValueBase
    {
        private object _jsonObject;

        /// <summary>
        /// 是否设置过JSON对象
        /// </summary>
        public virtual bool HasSetJsonObject { get; private set; }

        /// <summary>
        /// 需要序列化为JSON的对象
        /// </summary>
        public object JsonObject
        {
            get { return _jsonObject; }
            set
            {
                HasSetJsonObject = true;
                _jsonObject = value;
            }
        }
        bool isResponseJsonObject = true;
        /// <summary>
        /// 是否将对象序列化成JSON(默认为true）
        /// </summary>
        public virtual bool IsResponseJsonObject
        {
            get { return isResponseJsonObject; }
            set { isResponseJsonObject = value; }
        }

        private JSONFormatHanding _JSONCommonFormatHanding = JSONFormatHanding.DateFormatString_yyyyMMddHHmmss;
        /// <summary>
        /// JSON格式化方式
        /// </summary>
        public JSONFormatHanding JSONCommonFormatHanding
        {
            get { return _JSONCommonFormatHanding; }
            set { _JSONCommonFormatHanding = value; }
        }
    }
}
