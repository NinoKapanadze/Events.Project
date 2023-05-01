using Events.API.Models;
using System.Text;

namespace Events.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private string path = @"C:\Users\hp\Desktop\ToDoList\ToDo.API\path.txt";

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context.Request);

            // await context.Response.WriteAsync("In Logger Class  "); აბრუნებს ახალ რესფონსს: მოცემულ სტრინგს

            await _next(context);
        }

        private async Task LogRequest(HttpRequest request)
        {
            var requestModel = new RequestModel
            {
                IP = request.HttpContext.Connection.RemoteIpAddress.ToString(),
                Scheme = request.Scheme,
                Host = request.Host.ToString(),
                Method = request.Method,
                Path = request.Path,
                IsSecured = request.IsHttps,
                QueryString = request.QueryString.ToString(),
                Body = await ReadRequestBody(request),
            };

            var toLog = $"{Environment.NewLine} logged from Middleware {Environment.NewLine}" +
                $"IP = {requestModel.IP}{Environment.NewLine}" +
                 $"Address = {requestModel.Scheme}{Environment.NewLine}" +
                 $"Method = {requestModel.Method}{Environment.NewLine}" +
                   $"Path = {requestModel.Path}{Environment.NewLine}" +
                     $"IsSecured = {requestModel.IsSecured}{Environment.NewLine}" +
                       $"QueryString = {requestModel.QueryString}{Environment.NewLine}" +
                       $"RequestBody = {requestModel.Body}{Environment.NewLine}" +
                       $"Time = {requestModel.RequestTime}{Environment.NewLine}";
            await File.AppendAllTextAsync(path, toLog);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[request.ContentLength ?? 0];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            return bodyAsText;
        }
    }
}