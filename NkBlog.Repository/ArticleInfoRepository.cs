using Microsoft.Extensions.Options;
using NkBlog.Entities;
using NkBlog.IRepository;
using NkBolg.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NkBlog.Repository
{
    public class ArticleInfoRepository : BaseRepository<ArticleInfo, int>, IArticleInfoRepository
    {
        public ArticleInfoRepository(IOptions<DapperOptions> options) : base(options)
        {
        }
    }
}
