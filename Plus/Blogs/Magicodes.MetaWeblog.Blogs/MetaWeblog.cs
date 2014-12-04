using System.Collections.Generic;
using CookComputing.XmlRpc;

namespace Magicodes.MetaWeblog.Blogs
{
    /// <summary>
    /// 接口说明 http://www.cnblogs.com/stubman/archive/2011/05/17/1954598.html
    /// </summary>
    public class MetaWeblog : XmlRpcService, IMetaWeblog
    {
        #region Public Constructors

        #endregion

        #region IMetaWeblog Members

        string IMetaWeblog.Test()
        {
            return "调用接口成功";
        }
        string IMetaWeblog.AddPost(string blogid, string username, string password,
            Post post, bool publish)
        {
            if (ValidateUser(username, password))
            {
                string id = string.Empty;

                // TODO: 请根据实际情况返回一个字符串，一般是Blog的ID。

                return id;
            }
            return "User is not valid!";
            // throw new XmlRpcFaultException(0, "User is not valid!");
        }

        bool IMetaWeblog.UpdatePost(string postid, string username, string password,
            Post post, bool publish)
        {
            if (ValidateUser(username, password))
            {
                bool result = false;

                // TODO: 请根据实际情况返回一个布尔值，表示是否更新成功。

                return result;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        Post IMetaWeblog.GetPost(string postid, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                Post post = new Post();

                // TODO: 请根据实际情况返回一个Struct { Struct是一个规范格式，
                //       格式就是Post的属性，注意category是一个数组，是这个Post所属的类别。
                //       如果类别不存在，服务器端将只处理存在的类别}。

                return post;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        CategoryInfo[] IMetaWeblog.GetCategories(string blogid, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                List<CategoryInfo> categoryInfos = new List<CategoryInfo>();

                // TODO: 请根据实际情况获取Blog的类别，并设置CategoryInfo。

                return categoryInfos.ToArray();
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        Post[] IMetaWeblog.GetRecentPosts(string blogid, string username, string password,
            int numberOfPosts)
        {
            if (ValidateUser(username, password))
            {
                List<Post> posts = new List<Post>();

                // TODO: 返回一个结构（struct）的数组（array）。 
                //       每一个Struct包含getPost返回值一样的结构。请设置后返回。 


                return posts.ToArray();
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        MediaObjectInfo IMetaWeblog.NewMediaObject(string blogid, string username, string password,
            MediaObject mediaObject)
        {
            if (ValidateUser(username, password))
            {
                MediaObjectInfo objectInfo = new MediaObjectInfo();

                // TODO: 返回一个数组 
                //       其中blogid、username、password分别代表Blog的id（注释：如果你有两个Blog，blogid指定你需要编辑的blog）、用户名和密码。
                //       struct必须包含name, type 和bits三个元素，当然也可以包含其他元素。 


                return objectInfo;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        bool IMetaWeblog.DeletePost(string key, string postid, string username, string password, bool publish)
        {
            if (ValidateUser(username, password))
            {
                bool result = false;

                // TODO:  请根据实际情况返回一个布尔值，表示是否删除成功。

                return result;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        BlogInfo[] IMetaWeblog.GetUsersBlogs(string key, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                List<BlogInfo> infoList = new List<BlogInfo>();

                // TODO: 请根据实际情况获取 当前用户 Blog 信息，并设置用户 Blog 信息。

                return infoList.ToArray();
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        UserInfo IMetaWeblog.GetUserInfo(string key, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                UserInfo info = new UserInfo();

                // TODO: 请根据实际情况获取 当前用户 信息，并设置用户 信息。

                return info;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        #endregion

        #region Private Methods

        private bool ValidateUser(string username, string password)
        {
            bool result = false;

            // TODO: Implement the logic to validate the user

            return result;
        }

        #endregion

       
    }
}
