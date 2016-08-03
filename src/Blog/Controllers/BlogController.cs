﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Blog.Models.PostViewModels;
using Blog.Models;
using Blog.Models.Account;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog.Models.Data;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private IPostService postService { get; } 

        public BlogController (IPostService repository)
        {
            this.postService = repository;
        }

        // GET: /<controller>/
       

        [HttpGet("")]
        public IActionResult Index()
        {
            return Pagination(1);
        }

        [HttpGet("[controller]/{page:int?}")]
        public IActionResult Pagination(int page)
        {
            var posts  = postService.GetAll();
            var result = postService.GetPagedPosts(posts);
                  
            return View("Index", result);
        }


        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost("[controller]/contact")]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var name = viewModel.Name;
                return RedirectToActionPermanent("SuccessContact", viewModel);
            }

            return View();
        }


    }
}
