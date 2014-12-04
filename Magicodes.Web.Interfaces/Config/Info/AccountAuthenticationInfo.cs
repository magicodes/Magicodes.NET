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
//        filename :AccountAuthenticationInfo
//        description :
//
//        created by 雪雁 at  2014/10/27 21:05:38
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Config.Info
{
    /// <summary>
    ///  OAuth账户配置
    /// </summary>
    [Description("OAuth账户配置")]
    [T4GenerationIgnoreAttribute]
    public class AccountAuthenticationInfo : ConfigBase
    {
        public AccountAuthenticationInfo()
            : base()
        {

        }

        List<AccountConfigInfo> _accountConfigInfoList = new List<AccountConfigInfo>()
        {
            new AccountConfigInfo()
            {
                Enable=false,
                AccountType=AccountTypes.QQ,
                Id="",
                Secret=""
            }
        };
        /// <summary>
        /// OAuth账户配置列表
        /// </summary>
        [Display(Name = "OAuth账户配置列表")]
        public List<AccountConfigInfo> AccountConfigInfoList
        {
            get { return _accountConfigInfoList; }
            set { _accountConfigInfoList = value; }
        }
    }
    /// <summary>
    /// 账户配置信息
    /// </summary>
    public class AccountConfigInfo
    {
        /// <summary>
        /// Id或Key
        /// </summary>
        [Display(Name = "Id或Key")]
        [Required]
        [StringLength(200)]
        public string Id { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        [Display(Name = "密钥")]
        [Required]
        [StringLength(500)]
        public string Secret { get; set; }
        /// <summary>
        /// 账户类型
        /// </summary>
        [Required]
        [Display(Name = "账户类型")]
        public AccountTypes AccountType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [Display(Name = "是否启用")]
        public bool Enable { get; set; }

    }
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountTypes
    {
        Microsoft = 0,
        QQ = 1,
        Google = 2,
        Twitter = 3,
        Facebook = 4,
    }
}
