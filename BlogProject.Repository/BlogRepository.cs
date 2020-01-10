using BlogProject.Domain;
using BlogProject.Domain.IRepository;
namespace BlogProject.Repository
{
    public sealed class BlogRepository : RepositoryBase<Blog>,IBlogRepository
    {}
}
