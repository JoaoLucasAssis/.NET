using Microsoft.AspNetCore.Mvc;

namespace MVC.Components;

public class SearchBoxViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _contextAccessor;

    public SearchBoxViewComponent(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public async Task<IViewComponentResult> InvokeAsync(string searchTerm = "")
    {
        var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

        return View(new
        {
            Controller = controller,
            SearchTerm = searchTerm
        });
    }
}
