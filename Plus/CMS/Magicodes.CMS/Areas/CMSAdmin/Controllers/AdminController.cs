using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magicodes.CMS.UnitOfWork;
using Magicodes.Core.Web.Controllers;
using Magicodes.Web.Interfaces.Config.Info;


//*************************************************************************************
// <copyright file="AdminController " company="Magicode Team">
//     copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Eyes</author>
// <summary>CMS 后台管理控制器</summary>
//*************************************************************************************
      
namespace Magicodes.CMS.Areas.CMSAdmin.Controllers
{
    public class AdminController : AdminControllerBase
    {
        private CMS_UnitOfWork unitOfWork;
        public CMS_UnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork==null)
                {
                    unitOfWork=new CMS_UnitOfWork();
                }
                return unitOfWork;
            }
        }
        public ActionResult Index()
        {
            var siteInfo = ApplicationContext.ConfigManager.GetConfig<SiteConfigInfo>();
            ViewBag.SiteInfo = siteInfo;
            ViewBag.Title = siteInfo.SiteName;
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.Drives = System.IO.DriveInfo.GetDrives().Where(p => p.IsReady);
            return View();
        }
        /// <summary>
        /// 栏目管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Channel()
        {
            return View();
        }
        /// <summary>
        /// 内容分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassType()
        {
            ViewBag.ChannelList=UnitOfWork.CMS_ChannelRepository.Get(w => true).Select(s => new SelectListItem()
            {
                Text = s.ChannelName,
                Value = s.Id.ToString()
            }).ToList();
            return View();
        }
        /// <summary>
        /// 评论管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Comment()
        {
            return View();
        }

        public ActionResult ContentTag()
        {
            return View();
        }

        public ActionResult Photo()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }
	}
}