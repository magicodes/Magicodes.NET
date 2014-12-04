using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :LoginViewModel
//        description :
//
//        created by 雪雁 at  2014/10/11 11:21:16
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Default.API.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "登录名或邮箱")]
        public string LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住密码？")]
        public bool RememberMe { get; set; }
    }
}
