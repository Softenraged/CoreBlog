﻿using Microsoft.AspNetCore.Mvc;
using CoreBlog.Web.Services;
using CoreBlog.Web.Factory;
using CoreBlog.Web.ViewModels.Post;
using CoreBlog.Web.ViewModels.Account;
using System.Threading.Tasks;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace CoreBlog.Web.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    [Route("")]
    public sealed class BlogController : Controller
    {
        private IMailService mailService { get; }
        private IPageService pageService { get; }
        private IPostService postService { get; } 

        public BlogController (IPostService postService, 
                               IPageService pageService,
                               IMailService mailService)
        {
            this.mailService = mailService;
            this.postService = postService;
            this.pageService = pageService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
           return Index(1);
        }


        [HttpGet("[controller]/{page:int?}")]
        public IActionResult Index(int page = 1)
        {
               
            var posts       = postService.GetAllPublished();
            var latestposts = postService.GetLatest(posts, 5);
            var pagedList   = pageService.GetPagedList(posts, page);

            //get a counter for each category
            ViewData["Development"] = postService.GetCategoryCounter(posts, "Development");
            ViewData["Managment"]   = postService.GetCategoryCounter(posts, "Managment");
            ViewData["Design"]      = postService.GetCategoryCounter(posts, "Design");
            ViewData["Other"]       = postService.GetCategoryCounter(posts, "Other");

            //get latest posts
            ViewBag.Latest = ModelFactory.Create<PostViewModel>( latestposts );

            var pageViewModel = ModelFactory.Create( pagedList );        
                  
            return View(pageViewModel);
        }

      
        [HttpGet("[controller]/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("[controller]/Contact")]
        [ValidateRecaptcha]
        public async Task<IActionResult> Contact(ContactViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await mailService.SendEmailAsync(viewModel);

                ModelState.Clear();
                ViewBag.Message = "Thanks for your feedback!";              
            }
            return View();
        }
    }
}
