using Serilog;
using System.Diagnostics;

namespace TemplateMiddleware.Templates;

public class TemplateLogMiddleware
{
    private readonly RequestDelegate _next;

    public TemplateLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        await _next(context);

        sw.Stop();

        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        Log.Information($"The application took was {sw.Elapsed.TotalMilliseconds}ms ({sw.Elapsed.TotalSeconds}s)");
    }
}
