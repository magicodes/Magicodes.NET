using Magicodes.Web.Interfaces.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :CommonBusinessModelBase
//        description :
//
//        created by 雪雁 at  2014/10/28 21:07:43
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Models
{
    /// <summary>
    /// 通用业务模型基类
    /// </summary>
    [Description("通用业务模型基类")]
    [Serializable]
    public class CommonBusinessModelBase<TKey, TUserKeyType> : ICommonBusinessModelBase<TKey, TUserKeyType>
    {
        public CommonBusinessModelBase()
        {
            CreateTime = DateTimeOffset.Now;
            Deleted = false;
        }
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [Display(Name = "主键Id")]
        [T4GenerationIgnoreAttribute]
        [ReadOnly(true)]
        public virtual TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [ReadOnly(true)]
        [T4GenerationIgnoreAttribute]
        public virtual DateTimeOffset CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [ReadOnly(true)]
        [T4GenerationIgnoreAttribute]
        public virtual DateTimeOffset? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        [ReadOnly(true)]
        [T4GenerationIgnoreAttribute]
        public virtual bool Deleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        [T4GenerationIgnoreAttribute]
        [ReadOnly(true)]
        [StringLength(128)]
        public virtual TUserKeyType CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Display(Name = "更新人")]
        [T4GenerationIgnoreAttribute]
        [ReadOnly(true)]
        [StringLength(128)]
        public virtual TUserKeyType UpdateBy { get; set; }
    }
}
