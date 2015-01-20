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
//        filename :T4DataType
//        description :
//
//        created by 雪雁 at  2014/10/27 21:39:28
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.T4.Models
{
    /// <summary>
    /// T4数据类型
    /// </summary>
    public enum T4DataType
    {
        // 摘要: 
        //     表示自定义的数据类型。
        Custom = 0,
        //
        // 摘要: 
        //     表示某个具体时间，以日期和当天的时间表示。
        DateTime = 1,
        //
        // 摘要: 
        //     表示日期值。
        Date = 2,
        //
        // 摘要: 
        //     表示时间值。
        Time = 3,
        //
        // 摘要: 
        //     表示对象存在的一段连续时间。
        Duration = 4,
        //
        // 摘要: 
        //     表示电话号码值。
        PhoneNumber = 5,
        //
        // 摘要: 
        //     表示货币值。
        Currency = 6,
        //
        // 摘要: 
        //     表示所显示的文本。
        Text = 7,
        //
        // 摘要: 
        //     表示一个 HTML 文件。
        Html = 8,
        //
        // 摘要: 
        //     表示多行文本。
        MultilineText = 9,
        //
        // 摘要: 
        //     表示电子邮件地址。
        EmailAddress = 10,
        //
        // 摘要: 
        //     表示密码值。
        Password = 11,
        //
        // 摘要: 
        //     表示 URL 值。
        Url = 12,
        //
        // 摘要: 
        //     表示图像的 URL。
        ImageUrl = 13,
        //
        // 摘要: 
        //     表示信用卡号码。
        CreditCard = 14,
        //
        // 摘要: 
        //     表示邮政代码。
        PostalCode = 15,
        //
        // 摘要: 
        //     表示文件上载数据类型。
        Upload = 16,
        /// <summary>
        /// Bool类型的值
        /// </summary>
        Bit = 17,
        /// <summary>
        /// 整数
        /// </summary>
        Integer = 18,
        /// <summary>
        /// 小数
        /// </summary>
        Double = 19,
        /// <summary>
        /// 复选框列表
        /// </summary>
        CheckBoxList = 20,
        /// <summary>
        /// 单选框列表
        /// </summary>
        RadioBoxList = 22,
        /// <summary>
        /// 下拉列表
        /// </summary>
        Select = 23
    }
}
