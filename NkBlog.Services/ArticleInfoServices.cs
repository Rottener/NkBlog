using NkBlog.Entities;
using NkBlog.IRepository;
using NkBlog.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace NkBlog.Services
{
    public class ArticleInfoServices:BaseService<ArticleInfo,int>, IArticleInfoServices
    {
        private readonly IArticleInfoRepository _articleInfoRepository;
        public ArticleInfoServices(IArticleInfoRepository articleInfoRepository):base(articleInfoRepository)
        {
            _articleInfoRepository = articleInfoRepository;
        }
    }
}
