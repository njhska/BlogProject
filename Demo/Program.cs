using System;
using BlogProject.Common;
using BlogProject.Domain;
using BlogProject.Repository;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 0;
            while (true)
            {
                Blog b = new Blog("proeee"+n, "345", "456", new LookupItem("1", "2", 0));
                BlogRepository repository = new BlogRepository();
                var model = repository.Get("proeee"+n);
                if (model == null)
                {
                    repository.Create(b);
                }
                model = repository.Get("proeee"+n);
                model.ChangeTitle("title");
                model.ChangeActive(false);
                repository.Modify(model);
                n++;
            }
            
        }
    }
}
