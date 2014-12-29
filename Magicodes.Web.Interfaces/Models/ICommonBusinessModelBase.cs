using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :IModelsUtil
//        description :
//
//        created by 雪雁 at  2014/10/28 20:53:06
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Models
{
    /// <summary>
    /// 通用模型接口
    /// </summary>
    public interface ICommonBusinessModelBase<TKey, TUserKeyType>
    {
        TKey Id { get; set; }
        TUserKeyType CreateBy { get; set; }
        TUserKeyType UpdateBy { get; set; }
        DateTimeOffset CreateTime { get; set; }
        DateTimeOffset? UpdateTime { get; set; }
        bool Deleted { get; set; }
    }
}
