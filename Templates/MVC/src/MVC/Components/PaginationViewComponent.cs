using Microsoft.AspNetCore.Mvc;

namespace MVC.Components;

public class PaginationViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int totalItems, int currentPage, int itemsPerPage)
    {
        int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

        return View(new
        {
            CurrentPage = currentPage,
            TotalPages = totalPages
        });
    }
}