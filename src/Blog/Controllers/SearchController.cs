﻿using Blog.Models;
using Blog.Models.Data;
using Blog.Service;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("[controller]/")]
    public class SearchController : Controller
    {
        private IPageService pageService { get; }

        private IPostService postService { get; }

        public SearchController(IPostService postService, IPageService pageService)
        {
            this.postService = postService;
            this.pageService = pageService;
        }

        //using prefix Item2 for correct model binding from tuple
        [HttpPost]
        public IActionResult GetString(SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                return RedirectPermanent($"Search/{search.Text}");
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public IActionResult SearchResult()
        {
            return View();
        }

  
        [HttpGet("{text}/{page:int?}")]
        public IActionResult Pagination(string text = null, int page = 1)
        {

            if(string.IsNullOrEmpty(text))
            {
                ViewBag.text = null;
                return RedirectToAction("SearchResult");
            }

            pageService.InitialPage = page;

            ViewBag.text = text;

            var posts  = postService.FindPostsByText(text);
            var pagedList = pageService.GetPagedList(posts);
            var result = ModelFactory.Create(pagedList);

            

            return View("SearchResult", result);

        }

    }
}
