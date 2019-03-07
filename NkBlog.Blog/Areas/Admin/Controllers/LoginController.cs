using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NkBlog.Entities.DTO;
using NkBlog.IServices;
using NkBolg.Common.Auth;
using NkBolg.Common.Log;
using NkBolg.Common.Utils;

namespace NkBlog.Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly ISysAccountServices _sysAccountServices;
        public LoginController(ISysAccountServices sysAccountServices)
        {
            _sysAccountServices = sysAccountServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Login(string userName, string password, string code)
        {
            OperateResult<string> result = new OperateResult<string>();
            string verifyCode = HttpContext.Session.GetString("VerifyCode");
            if (verifyCode == null)
            {
                result.Message = "验证码已过期";
            }
            else if (code.ToLower() != verifyCode.ToString().ToLower())
            {
                result.Message = "验证码有误";
            }
            else
            {
                //清除验证码
                HttpContext.Session.Remove("VerifyCode");
                var operateResult = _sysAccountServices.Login(userName, password);
                //var operateResult = _sysAccountServices.AccountDetail(userName);
                AuthorizationUser auth = operateResult.Data;
                if (auth != null)
                {
                    await AuthenticationHelper.SetAuthCookie(auth);
                    result.Status = ResultStatus.Success;
                    result.Data = "/Admin/Home/Index";

                    #region 记录登录日志
                    LoginLogHandler loginLog = new LoginLogHandler(auth.LoginId);
                    loginLog.WriteLog();
                    #endregion
                }
                result.Message = operateResult.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileContentResult ValidateCode()
        {
            VerifyCodeUtil code = new VerifyCodeUtil();
            code.SetHeight = 36;
            code.SetForeNoisePointCount = 4;
            var byteImg = code.GetVerifyCodeImage();
            HttpContext.Session.SetString("VerifyCode", code.SetVerifyCodeText);
            return File(byteImg, @"image/jpeg");
        }
    }
}