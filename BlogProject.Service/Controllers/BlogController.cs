using System;
using System.Collections.Generic;
using System.Web.Http;
using BlogProject.Common;
using BlogProject.Domain;
using BlogProject.Domain.IRepository;

namespace BlogProject.Service.Controllers
{
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
            var result = repository.Get(id);
            result.Hits();
            repository.Modify(result);
            return result;
        }

        //分页
        [HttpGet]
        public List<Blog> FindBlogs()
        {
            var sql = "select data::text from blog limit 10";
            return repository.Find(sql);
        }

        [Authorize]
        public IHttpActionResult ModifyBlog(Blog blog)
        {
            var model = repository.Get(blog.Id);

            model.ChangeTitle(blog.Title);
            model.ChangeContent(blog.Content);
            model.ChangeBlogType(blog.BlogType);

            repository.Modify(model);

            return Ok();
        }

        public IHttpActionResult LeaveMessage(string id,[FromBody] IList<LeaveMessage> leaveMessages)
        {
            var model = repository.Get(id);
            model.UpdateLeaveMessages(leaveMessages);

            repository.Modify(model);

            return Ok();
        }

        [Authorize]
        public IHttpActionResult ClearMessages(string id)
        {
            var model = repository.Get(id);
            model.ClearLeaveMessages();

            repository.Modify(model);

            return Ok();
        }
    }
}
