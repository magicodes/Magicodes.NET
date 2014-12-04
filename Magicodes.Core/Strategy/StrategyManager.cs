using Magicodes.Web.Interfaces.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Strategy
{
    public class StrategyManager : StrategyManagerBase
    {
        /// <summary>
        /// 获取默认策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T GetDefaultStrategy<T>()
        {
            return GetDefaultStrategy<T>(typeof(T).FullName);
        }
        /// <summary>
        /// 添加策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public override T AddStrategy<T>(T t)
        {
            AddStrategy<T>(typeof(T).FullName, t);
            return t;
        }
        /// <summary>
        /// 添加策略
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public override T AddStrategy<T>(string key, T t)
        {
            if (StrategyDictionary.ContainsKey(key))
            {
                StrategyDictionary[key].Add(t);
            }
            else
            {
                var newValue=new List<IStrategyBase>() { t };
                StrategyDictionary.AddOrUpdate(key, newValue, (tKey, existingVal) =>
                {
                    return newValue;
                });
            }
            return t;
        }
        public override T GetDefaultStrategy<T>(string key)
        {
            if (StrategyDictionary.ContainsKey(key))
                return (T)StrategyDictionary[key].First();
            return default(T);
        }

        public override List<T> GetStrategys<T>(string key)
        {
            if (StrategyDictionary.ContainsKey(key))
                return StrategyDictionary[key] as List<T>;
            return null;
        }

        public override List<T> GetStrategys<T>()
        {
            return GetStrategys<T>(typeof(T).FullName);
        }

        public override T AddDefaultStrategy<T>(string key, T t)
        {
            if (StrategyDictionary.ContainsKey(key))
            {
                StrategyDictionary[key].Add(t);
                //反转元素，使后来居上
                StrategyDictionary[key].Reverse();
            }
            else
            {
                var newValue = new List<IStrategyBase>() { t };
                StrategyDictionary.AddOrUpdate(key, newValue, (tKey, existingVal) =>
                {
                    return newValue;
                });
            }
            return t;
        }
    }
}
