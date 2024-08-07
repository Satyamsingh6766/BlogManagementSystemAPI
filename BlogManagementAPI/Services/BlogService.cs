using BlogManagementAPI.Models;
using BlogManagementAPI.Repository;

namespace BlogManagementAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Response<BlogModel> GetBlog(int id)
        {
            return _blogRepository.GetBlog(id);
        }
        public Response<List<BlogModel>> GetAllBlogs()
        {
            return _blogRepository.GetAllBlogs();
        }
        public Response<BlogModel> Create(BlogModel blogModel)
        {
            return _blogRepository.Create(blogModel);
        }

        public Response<BlogModel> Update(BlogModel blogModel)
        {
            return _blogRepository.Update(blogModel);
        }

        public Response<BlogModel> Delete(int id)
        {
            return _blogRepository.Delete(id);
        }


    }
}
