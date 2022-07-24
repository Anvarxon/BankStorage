namespace BankStorage.Api.Interfaces
{
    public interface IImageUploadService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
