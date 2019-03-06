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
            string ency= EncryptUtil.MD5Encrypt32("1059727415166767104" + "123456");
            var operateResult = _sysAccountServices.Login("root","123456");
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
