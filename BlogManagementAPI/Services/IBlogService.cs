using BlogManagementAPI.Models;

namespace BlogManagementAPI.Services
{
    public interface IBlogService
    {
        Response<List<BlogModel>> GetAllBlogs();
        Response<BlogModel> GetBlog(int id);
        Response<BlogModel> Create(BlogModel blogModel);
        Response<BlogModel> Update(BlogModel blogModel);
        Response<BlogModel> Delete(int id);
    }
}
