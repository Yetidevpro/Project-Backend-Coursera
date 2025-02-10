using System.Text;

public class InputValidationMiddleware
{
    private readonly RequestDelegate _next;

    public InputValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Request body cannot be empty.");
                return;
            }
        }

        await _next(context);
    }
}
