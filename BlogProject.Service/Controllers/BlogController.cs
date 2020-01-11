using System;
using System.Collections.Generic;
using System.Web.Http;
using BlogProject.Common;
using BlogProject.Domain;
using BlogProject.Domain.IRepository;

namespace BlogProject.Service.Controllers
{
    [Authorize]
    public class BlogController : ApiController
    {
        private readonly IBlogRepository repository;
        public BlogController(IBlogRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public Blog GetBlog(string id)
        {
            return repository.Get(id);
        }

        [HttpGet]
        public List<Blog> FindBlogs()
        {
            var sql = "select data::text from blog limit 10";
            return repository.Find(sql);
        }
    }
}
