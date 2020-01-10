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
            Blog b = new Blog("proeee", "345", "456", new LookupItem("1", "2", 0));
            BlogRepository repository = new BlogRepository();
            //repository.Create(b);
            var model = repository.Get("proeee");
            Console.ReadKey();
        }
    }
}
