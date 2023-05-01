using Newtonsoft.Json;

namespace Events.API.MiddleWare
{
    public class MyExceptionHandlerMiddleware
    {
         
         private readonly RequestDelegate _next;

        public MyExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                //   Log.Error(ex.Message, ex);
                //  var mylogger = new   ApiError(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var error = new ApiError(context, ex);
            var result = JsonConvert.SerializeObject(error);

            context.Response.Clear();
            context.Response.ContentType = "application /json";
            context.Response.StatusCode = error.Status.Value;

            await context.Response.WriteAsync(result);

        }
    }
}
