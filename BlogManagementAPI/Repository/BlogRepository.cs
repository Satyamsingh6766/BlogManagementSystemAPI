using BlogManagementAPI.Models;
using Newtonsoft.Json;
using System.IO;

namespace BlogManagementAPI.Repository
{
    public class BlogRepository : IBlogRepository
    {

        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "blogsData.json");
        private List<BlogModel> _blogModels;


        public BlogRepository()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _blogModels = JsonConvert.DeserializeObject<List<BlogModel>>(json) ?? new List<BlogModel>();
            }
            else
            {
                _blogModels = new List<BlogModel>();
            }
        }

        public Response<List<BlogModel>> GetAllBlogs()
        {
            Response<List<BlogModel>> response = new Response<List<BlogModel>>();
            try
            {
                response.Data = _blogModels.OrderByDescending(x=>x.DateCreated).ToList();
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<BlogModel> GetBlog(int id)
        {
            Response<BlogModel> response = new Response<BlogModel>();
            BlogModel blogModel = new BlogModel();
            try
            {
                blogModel = _blogModels.FirstOrDefault(data => data.Id == id);
                if (blogModel != null)
                {
                    response.Data = blogModel;
                    response.Success = true;
                }
                else
                {
                    response.Message = "Blog not found";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
        public Response<BlogModel> Create(BlogModel blogModel)
        {
            Response<BlogModel> response = new Response<BlogModel>();
            try
            {
                blogModel.Id = _blogModels.Any() ? _blogModels.Max(data => data.Id) + 1 : 100;
                blogModel.DateCreated = DateTime.Now;
                _blogModels.Add(blogModel);
                SaveChanges();
                response.Success = true;
                response.Data = blogModel;
                response.Message = "Blog created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<BlogModel> Update(BlogModel blogModel)
        {
            Response<BlogModel> response = new Response<BlogModel>();
            try
            {
                BlogModel existingModel = _blogModels.FirstOrDefault(data => data.Id == blogModel.Id);
                if (existingModel != null)
                {
                    existingModel.UserName = blogModel.UserName;
                    existingModel.Text = blogModel.Text;
                    SaveChanges();
                    response.Success = true;
                    response.Data = existingModel;
                    response.Message = "Blog updated successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Blog not found";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
        


        public Response<BlogModel> Delete(int id)
        {
            Response<BlogModel> response = new Response<BlogModel>();
            try
            {
                var blogModel = _blogModels.FirstOrDefault(data => data.Id == id);
                if (blogModel != null)
                {
                    _blogModels.Remove(blogModel);
                    SaveChanges();
                    response.Success = true;
                    response.Data = blogModel;
                    response.Message = "Blog deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Data = blogModel;
                    response.Message = "Blog not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_blogModels, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }


    }
}
