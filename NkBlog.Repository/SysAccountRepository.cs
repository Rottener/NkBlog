using Dapper;
using Microsoft.Extensions.Options;
using NkBlog.Entities;
using NkBlog.Entities.DTO;
using NkBlog.IRepository;
using NkBolg.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NkBlog.Repository
{
    public class SysAccountRepository : BaseRepository<SysAccount, string>, ISysAccountRepository
    {
        public SysAccountRepository(IOptions<DapperOptions> options) : base(options)
        {
        }

        public AccountDetailsDto AccountDetail(string accountId)
        {
            string sql = "select * from sysaccount as a inner join sysuser as u on a.AccountId=u.AccountId";
            var s = _dbConnection.Query<SysAccount, SysUser, AccountDetailsDto, AccountDetailsDto>(sql, (a, u,ad) =>
            {
                a.AccountId = u.AccountId;
                return ad;
            }, splitOn: "UserName,Password").First();
            return s;
        }

        public SysAccount AccountDetail(Expression<Func<SysAccount, bool>> expression)
        {
            string sql = "select * from sysaccount as a inner join sysuser as u on a.AccountId=u.AccountId";
            var s = _dbConnection.Query<SysAccount, SysUser, SysAccount>(sql, (a, u) =>
            {
                a.AccountId = u.AccountId;
                return a;
            }, splitOn: "UserName,Password").First();
            return s;
        }
    }
}
