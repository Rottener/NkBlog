using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NkBlog.Blog.Models;
using NkBlog.IServices;
using NkBolg.Common.Utils;

namespace NkBlog.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleInfoServices _articleInfoServices;
        private readonly ISysAccountServices _sysAccountServices;
        public HomeController(IArticleInfoServices articleInfoServices, ISysAccountServices sysAccountServices)
        {
            _articleInfoServices = articleInfoServices;
            _sysAccountServices = sysAccountServices;
        }
        public IActionResult Index()
        {
            var d = new DateTime(2019, 1, 2);
            var c = DateTime.Now;
            var f= c - d;
            var day= f.Days;
            string ssss= EncryptUtil.MD5Encrypt32("1059728142672990208" + "123456");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
