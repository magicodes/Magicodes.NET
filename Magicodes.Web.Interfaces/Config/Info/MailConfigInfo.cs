using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Config.Info
{
    /// <summary>
    /// 邮箱配置
    /// </summary>
    [Description("邮箱信息配置")]
    public class MailConfigInfo : ConfigBase
    {
        /// <summary>
        /// SMTP服务器
        /// </summary>
        [Display(Name = "SMTP服务器")]
        [Required]
        [StringLength(50)]
        public string SmtpServer { get; set; }
        /// <summary>
        /// SMTP服务器端口
        /// </summary>
        [Display(Name = "SMTP服务器端口")]
        [Required]
        [Range(0, 65535)]
        public int SmtpPort { get; set; }
        [Display(Name = "是否启用SSL验证")]
        public bool EnableSsl { get; set; }
        /// <summary>
        /// 发送邮箱
        /// </summary>
        [Display(Name = "发送邮箱")]
        [EmailAddress]
        [MaxLength(300)]
        [Required]
        public string MailFrom { get; set; }
        /// <summary>
        /// 邮件发送昵称
        /// </summary>
        [Display(Name = "邮件发送昵称")]
        [MaxLength(50)]
        public string FromNickName { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        [Display(Name = "用户账户")]
        [MaxLength(300)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "用户密码")]
        [MaxLength(300)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "域")]
        [MaxLength(300)]
        public string Domain { get; set; }
    }
}
