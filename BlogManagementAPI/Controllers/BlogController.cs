using BlogManagementAPI.Models;
using BlogManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementAPI.Controllers
{
    [ApiController]
    [Route("api/blog")]

    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("getAllBlogs")]
        public IActionResult GetAllBlogs()
        {
            var response = _blogService.GetAllBlogs();
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }

        [HttpGet("getBlogById/{id}")]
        public IActionResult GetBlog(int id)
        {
            var response = _blogService.GetBlog(id);
            if (response.Success)
            {
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response.Message);
                }                
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }

        [HttpPost("createBlog")]
        public IActionResult CreateBlog([FromBody] BlogModel blogModel)
        {
            if (string.IsNullOrEmpty(blogModel.UserName) || string.IsNullOrEmpty(blogModel.Text))
            {
                return BadRequest(new { Message = "Username and Text are required." });
            }

            var response = _blogService.Create(blogModel);
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);

        }

        [HttpPut("updateBlog/{id}")]
        public ActionResult UpdateBlog(int id, [FromBody] BlogModel blogModel)
        {
            if (string.IsNullOrEmpty(blogModel.UserName) || string.IsNullOrEmpty(blogModel.Text))
            {
                return BadRequest(new { Message = "Username and Text are required." });
            }

            var response = _blogService.GetBlog(id);
            if (!response.Success || response.Data == null)
            {
                return NotFound(response.Message);
            }

            blogModel.Id = id;
            var updateResponse = _blogService.Update(blogModel);
            if (updateResponse.Success)
            {
                return Ok(updateResponse);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, updateResponse.Message);

        }

        [HttpDelete("deleteBlog/{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var response = _blogService.GetBlog(id);
            if (!response.Success || response.Data == null)
            {
                return NotFound(response.Message);
            }

            var deleteResponse = _blogService.Delete(id);
            if (deleteResponse.Success)
            {
                return Ok(deleteResponse);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, deleteResponse.Message);
        }


    }
}
