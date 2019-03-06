using NkBlog.Entities.DTO;
using NkBlog.IRepository;
using NkBolg.Common.Auth;
using NkBolg.Common.Extensions;
using NkBolg.Common.Utils;
using System;

namespace NkBlog.Services
{
    public class SysAccountLogin
    {
        private readonly ISysAccountRepository _sysAccountRepository;
        public SysAccountLogin(ISysAccountRepository sysAccountRepository)
        {
            _sysAccountRepository = sysAccountRepository;
        }
        public OperateResult<AuthorizationUser> Login(string account, string password)
        {
            var accountDetail = _sysAccountRepository.AccountDetail(a => a.UserName == account);
            OperateResult<AuthorizationUser> result = new OperateResult<AuthorizationUser>();
            if (accountDetail == null)
            {
                result.Message = "用户名或密码错误";
                return result;
            }
            if (accountDetail.Password != EncryptUtil.MD5Encrypt32(accountDetail.AccountId + password))
            {
                result.Message = "用户名或密码错误";

            }
            else if (!accountDetail.EnabledMark)
            {
                result.Message = "登录账户已被禁用";
            }
            else
            {
                result.Status = ResultStatus.Success;
                //var user = accountDetail.MapTo<AuthorizationUser, AccountDetailsDto>();
                //user.LoginId = new Guid().ToString();
                //user.HeadIcon = user.HeadIcon ?? "/images/default.png";
                //result.Data = user;
            }
            return result;
        }
    }
}
