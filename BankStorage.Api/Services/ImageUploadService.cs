using BankStorage.Api.Interfaces;

namespace BankStorage.Api.Services
{
    public class ImageUploadService : IImageUploadService
    {
        public IWebHostEnvironment webHostEnvironment;
        
        public async Task<bool> UploadFile(IFormFile file)
        {
            try
            {
                if (file.Length == 1 && file.ContentType == "image/png")
                {
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    var newFileName = "Image_" + DateTime.Now.TimeOfDay.Milliseconds + fileInfo.Extension;

                    string path = Path.GetFullPath(Path.Combine(webHostEnvironment.ContentRootPath, "/UploadedFiles/" + newFileName));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return path;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Upload Failed", ex);
            }
        }
    }
}