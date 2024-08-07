namespace BlogManagementAPI.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
    }

    //public class BlogResponse : Response<BlogModel>
    //{
    //}

    //public class BlogListResponse : Response<List<BlogModel>>
    //{
    //}


}
