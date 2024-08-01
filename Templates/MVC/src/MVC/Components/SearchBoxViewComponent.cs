using Microsoft.AspNetCore.Mvc;

namespace MVC.Components;

public class SearchBoxViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string controller, string searchTerm = "")
    {
        return View(new
        {
            Controller = controller,
            SearchTerm = searchTerm
        });
    }
}
