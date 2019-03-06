using Dapper;
using Microsoft.Extensions.Options;
using NkBlog.Entities;
using NkBlog.IRepository;
using NkBolg.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NkBlog.Repository
{
    public class SysAccountRepository : BaseRepository<SysAccount, string>, ISysAccountRepository
    {
        public SysAccountRepository(IOptions<DapperOptions> options) : base(options)
        {
        }

        public List<SysAccount> ListSysAccount()
        {
            string sql = "select * from sysaccount as a inner join sysuser as u on a.AccountId=u.AccountId";
            var s = _dbConnection.Query<SysAccount, SysUser, SysAccount>(sql, (a, u) =>
            {
                a.AccountId = u.AccountId;
                return a;

            }, splitOn: "UserName,Password").ToList();
            return s;
        }
    }
}
