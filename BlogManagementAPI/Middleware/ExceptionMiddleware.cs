using Newtonsoft.Json;
using System.Net;
using BlogManagementAPI.Models;

namespace BlogManagementAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await ManageExceptionAsync(context, ex);
            }
        }

        private static Task ManageExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Response<string> response = new Response<string>();
            response.Message = $"Exception handled through middleware with error: {ex.Message}";
            var json = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(json);
        }
    }
}
