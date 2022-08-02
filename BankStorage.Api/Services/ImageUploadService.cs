using BankStorage.Api.Interfaces;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using FluentValidation;
using BankStorage.Domain.Models;

namespace BankStorage.Api.Services
{
    public class ImageUploadService : IImageUploadService
    {
        public IWebHostEnvironment webHostEnvironment;

        public async Task<string> UploadFile(IFormFile file)
        {
            
            try
            {
                if (file.Length > 0 && file.ContentType == "image/png")
                {
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    Bank bank = new Bank();
                    var newFileName = "Logo_" + DateTime.Now.TimeOfDay.Milliseconds + fileInfo.Extension;

                    string fileLocation = Path.GetFullPath(Path.Combine(webHostEnvironment.ContentRootPath, @"\UploadedFiles\"));
                    if (!Directory.Exists(fileLocation))
                    {
                        Directory.CreateDirectory(fileLocation);
                    }
                    
                    using (var fileStream = new FileStream(Path.Combine(fileLocation, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        bank.Logo = fileLocation;
                    }
                    return fileLocation;
                }
                else
                {
                    return "Ошибка при сохранении файла";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при сохранении файла", ex);
            }
        }
    }
}