using System;
using CookComputing.XmlRpc;

namespace Magicodes.MetaWeblog.Blogs
{
    /// <summary>
    /// 接口说明 http://www.cnblogs.com/stubman/archive/2011/05/17/1954598.html
    /// </summary>
    #region Structs

    public struct BlogInfo
    {
        public string Blogid;
        public string Url;
        public string BlogName;
    }

    public struct Category
    {
        public string CategoryId;
        public string CategoryName;
    }

    [Serializable]
    public struct CategoryInfo
    {
        public string Description;
        public string HtmlUrl;
        public string RssUrl;
        public string Title;
        public string Categoryid;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Enclosure
    {
        public int Length;
        public string Type;
        public string Url;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Post
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


    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Source
    {
        public string name;
        public string url;
    }

    public struct UserInfo
    {
        public string userid;
        public string firstname;
        public string lastname;
        public string nickname;
        public string email;
        public string url;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct MediaObject
    {
        public string name;
        public string type;
        public byte[] bits;
    }

    [Serializable]
    public struct MediaObjectInfo
    {
        public string url;
    }

    #endregion
}
