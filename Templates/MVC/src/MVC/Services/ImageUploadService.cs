using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVC.Services
{
    public class ImageUploadService : IImageUploadService
    {
        public async Task<bool> UploadProductImage(ModelStateDictionary modelState, IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            try
            {
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Empty, $"An error occurred while saving the file: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
