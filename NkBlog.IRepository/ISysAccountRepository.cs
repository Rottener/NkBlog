using NkBlog.Entities;
using NkBlog.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NkBlog.IRepository
{
    public interface ISysAccountRepository:IBaseRepository<SysAccount,string>
    {
        AccountDetailsDto AccountDetail(string accountId);

        SysAccount AccountDetail(Expression<Func<SysAccount, bool>> expression);
    }
}
