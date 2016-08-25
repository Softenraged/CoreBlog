﻿using System.Collections.Generic;
using Sakura.AspNetCore;
using CoreBlog.Web.ViewModels.Post;
using CoreBlog.Data.Entities;

namespace CoreBlog.Web.Services
{
    public interface IPageService
    {

        int PageSize { get; set; }
        IPagedList<PostViewModel> GetPagedList(IEnumerable<Post> posts, int pageIndex);
    }
}