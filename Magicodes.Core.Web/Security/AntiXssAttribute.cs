using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Globalization;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :AntiXssAttribute
//        description :AntiXss验证特性，防止XSS攻击
//        Demo:
//              [AntiXss]
//              public string Name { get; set; }
//
//              [AntiXss(allowedStrings: "<br />,<p>")]
//              public string Description { get; set; }
//
//              [AntiXss(allowedStrings: "<br />", disallowedStrings:"/, #")]
//              public string NoSlashesOrHashes { get; set; }
//
//              [AntiXss(errorMessage: "This is a custom error message")]
//              public string CustomError { get; set; }
//
//              [AntiXss(errorMessageResourceName:"TestMessage", errorMessageResourceType: typeof(TestResources))]
//              public string ResourceCustomError { get; set; }
//
//        created by 雪雁 at  2014/11/17 11:02:52
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Security
{
    
    /// <summary>
    /// AntiXss验证特性，防止XSS攻击
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AntiXssAttribute : ValidationAttribute
    {
        const string DefaultValidationMessageFormat = "字段 {0} XSS验证失败，请检查输入的字符串中是否含有非法字符。";
        private readonly string errorMessage;
        private readonly string errorMessageResourceName;
        private readonly Type errorMessageResourceType;
        private readonly string allowedStrings;
        private readonly string disallowedStrings;
        private readonly Dictionary<string, string> allowedStringsDictionary;

        /// <summary>
        /// 初始化 <see cref="AntiXssAttribute"/> 的新实例.
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorMessageResourceName">获取或设置错误消息资源的名称，在验证失败的情况下，要使用该名称来查找 ErrorMessageResourceType 属性值</param>
        /// <param name="errorMessageResourceType">获取或设置在验证失败的情况下用于查找错误消息的资源类型。</param>
        /// <param name="allowedStrings">以逗号分隔的允许的字符串。</param>
        /// <param name="disallowedStrings">以逗号分隔的字符串不允许的字符或单词</param>
        public AntiXssAttribute(
            string errorMessage = null,
            string errorMessageResourceName = null,
            Type errorMessageResourceType = null,
            string allowedStrings = null,
            string disallowedStrings = null)
        {
            this.errorMessage = errorMessage;
            this.errorMessageResourceName = errorMessageResourceName;
            this.errorMessageResourceType = errorMessageResourceType;
            this.allowedStrings = allowedStrings;
            this.disallowedStrings = disallowedStrings;
            allowedStringsDictionary = new Dictionary<string, string>();
        }
        /// <summary>
        /// 确定对象的指定值是否有效。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return true;
        }
        /// <summary>
        /// 确定对象的指定值是否有效。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return base.IsValid(null, validationContext);
            }
            
            var encodedValue = EncoderHelper.HtmlEncode(value.ToString(), false);

            if (EncodedStringAndValueAreDifferent(value, encodedValue))
            {
                SetupAllowedStringsDictionary();

                foreach (var allowedString in allowedStringsDictionary)
                {
                    encodedValue = encodedValue.Replace(allowedString.Value, allowedString.Key);
                }

                if (EncodedStringAndValueAreDifferent(value, encodedValue))
                {
                    return new ValidationResult(SetErrorMessage(validationContext));
                }
            }

            if (!string.IsNullOrWhiteSpace(disallowedStrings) 
                && disallowedStrings.Split(',').Select(x => x.Trim()).Any(x => value.ToString().Contains(x)))
            {
                return new ValidationResult(SetErrorMessage(validationContext));
            }

            return base.IsValid(value, validationContext);
        }

        private static bool EncodedStringAndValueAreDifferent(object value, string encodedValue)
        {
            return !value.ToString().Equals(encodedValue);
        }

        private void SetupAllowedStringsDictionary()
        {
            if (string.IsNullOrWhiteSpace(allowedStrings))
            {
                return;
            }

            foreach (var allowedString in allowedStrings.Split(',').Select(x => x.Trim())
                .Where(allowedString => !allowedStringsDictionary.ContainsKey(allowedString)))
            {
                allowedStringsDictionary.Add(allowedString,
                    EncoderHelper.HtmlEncode(allowedString, false));
            }
        }
        /// <summary>
        /// 设置错误消息
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        private string SetErrorMessage(ValidationContext validationContext)
        {
            if (IsResourceErrorMessage())
            {
                var resourceManager = new ResourceManager(errorMessageResourceType);
                return resourceManager.GetString(errorMessageResourceName, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }

            return string.Format(DefaultValidationMessageFormat, validationContext.DisplayName);
        }

        private bool IsResourceErrorMessage()
        {
            return !string.IsNullOrEmpty(errorMessageResourceName) && errorMessageResourceType != null;
        }
    }
}
