using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NkBlog.Entities.DTO;
using NkBolg.Common.Auth;
using NkBolg.Common.Log;

namespace NkBlog.Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
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
                var operateResult = _sysAccountLogic.Login(userName, password);
                AuthorizationUser auth = operateResult.Data;
                if (auth != null)
                {
                    await AuthenticationHelper.SetAuthCookie(auth);
                    result.Status = ResultStatus.Success;
                    result.Data = "/Main/Home/Index";

                    #region 记录登录日志
                    LoginLogHandler loginLog = new LoginLogHandler(auth.LoginId);
                    loginLog.WriteLog();
                    #endregion
                }
                result.Message = operateResult.Message;
            }
            return Json(result);
        }
    }
}