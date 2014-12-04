using System;
using Magicodes.Models.Blog.BlogData;
using Magicodes.Models.Blog.Models.Post;

namespace Magicodes.Blogs.Blog.BLL
{
    public class PostBLL
    {
        #region 发布博客
        /// <summary>
        /// 发布博客 
        /// Author Anton
        /// DateTime 2014年11月10日 20:30:30
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public int PublishPost(BlogPost post)
        {
            if (post == null) throw new ArgumentNullException("post");
            var postRepository = new BaseRepository<BlogPost>();
            var postSubjectRepository = new BaseRepository<BlogPostSubject>();
            var postTagRepository = new BaseRepository<BlogPostTag>();
            int postId;
            try
            {
                postId = postRepository.AddEntity(post, true).Id;
            }
            catch (Exception ex)
            {
                throw new Exception("发布博客失败："+ex.Message);
            }
            //添加多个标签
            foreach (int tagId in post.BlogTagIds)
            {
                postTagRepository.AddEntity(
                new BlogPostTag
                {
                    PostId = postId,
                    BlogTagId = tagId,
                    CreateTime = DateTime.Now,
                    Deleted = false
                });
            }
            
            //postSubjectRepository.AddEntity(
            //    new BlogPostSubject
            //    {
            //        PostId = postId,
            //        SubjectId = 1,
            //        CreateTime = DateTime.Now,
            //        Deleted = false
            //    });
            try
            {
                return DbCommit.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("添加博客标签失败："+ex.Message);
            }


        }
        #endregion
    }
}