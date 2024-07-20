using TemplateMiddleware.Templates;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseMiddleware<TemplateLogMiddleware>();

app.MapGet("/", () => "Hello, World!");
app.MapGet("/test-log", () =>
{
    Thread.Sleep(1500);
    return "Middleware Testing";
});

app.Run();
