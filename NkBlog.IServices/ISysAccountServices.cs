using NkBlog.Entities;
using NkBlog.Entities.DTO;
using NkBolg.Common.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace NkBlog.IServices
{
    public interface ISysAccountServices:IBaseService<SysAccount,string>
    {
        AccountDetailsDto AccountDetail(string accountId);

        OperateResult<AuthorizationUser> Login(string account, string password);
    }
}
