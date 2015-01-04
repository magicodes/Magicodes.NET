using Magicodes.Web.Interfaces.Data;
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
//        filename :PagedList
//        description :
//
//        created by 雪雁 at  2014/12/24 10:26:09
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int total)
        {
            TotalCount = total;
            TotalPages = total / pageSize;
            if (total % pageSize > 0)
                TotalPages++;
            PageSize = pageSize;
            PageIndex = pageIndex;
            //AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
            AddRange(source);
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 分页数
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; private set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}
