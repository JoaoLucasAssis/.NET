using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVC.Services;

public interface IImageUploadService
{
    public Task<bool> UploadProductImage(ModelStateDictionary modelState, IFormFile file, string imgPrefix);
}
