using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Magicodes.Utility
{
    public class StackHelper
    {
        /// <summary>
        /// 获取当前调用的函数
        /// </summary>
        /// <returns></returns>
        public static MethodBase GetCallingMethod()
        {
            return new StackFrame(2, false).GetMethod();
        }
        /// <summary>
        /// 获取当前调用的类型
        /// </summary>
        /// <returns></returns>
        public static Type GetCallingType()
        {
            return new StackFrame(2, false).GetMethod().DeclaringType;
        }
        /// <summary>
        /// 获取当前函数
        /// </summary>
        /// <returns></returns>
        public static MethodBase GetCurrentMethod()
        {
            return new StackFrame(1).GetMethod();
        }
        /// <summary>
        /// 获取当前类
        /// </summary>
        /// <returns></returns>
        public static Type GetCurrentClassName()
        {
            return new StackFrame(1).GetMethod().DeclaringType;
        }
    }
}
