using System.ComponentModel.DataAnnotations;

namespace BlogManagementAPI.Models
{
    public class BlogModel
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
    }
}
