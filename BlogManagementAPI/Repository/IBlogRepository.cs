using BlogManagementAPI.Models;

namespace BlogManagementAPI.Repository
{
    public interface IBlogRepository
    {
        Response<List<BlogModel>> GetAllBlogs();
        Response<BlogModel> GetBlog(int id);
        Response<BlogModel> Create(BlogModel blogModel);
        Response<BlogModel> Update(BlogModel blogModel);
        Response<BlogModel> Delete(int id);
    }
}
