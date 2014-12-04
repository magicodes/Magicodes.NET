using System.Reflection;
using CookComputing.XmlRpc;
using System;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//        description :调用MetaWeblogAPI的测试方法
//        created by Anton at 2014年11月22日 16:33:51
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.MetaWeblog.Blogs
{
    [XmlRpcUrl("http://localhost:port/MetaWeblogAPI.ashx")]//MetaWeblogAPI的服务地址
    public class MetaWeblogProxy : XmlRpcClientProtocol
    {
        [XmlRpcBegin("metaWeblog.newPost")]
        public string AddPost(string blogId, string userName, string password, NewPost post, bool isSend)
        {
            //参数的数量需要对应
            var param = new object[] { blogId, userName, password, post, isSend };
            var value = Invoke(MethodBase.GetCurrentMethod().Name, param);
            return value == null ? null : value.ToString();
        }
        //结构参数
        public struct NewPost
        {
            public DateTime dateCreated;
            public string description;
            public string title;
            public string[] categories;
            public string permalink;
            public object postid;
            public string userid;
            public string wp_slug;
        }
    }
}
