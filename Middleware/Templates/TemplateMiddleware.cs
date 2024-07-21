namespace TemplateMiddleware.Templates;

public class TemplateMiddleware
{
    private readonly RequestDelegate _next;

    public TemplateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Do something first

        // Call the next middleware in the pipeline
        await _next(context);

        // Do something later
    }
}
