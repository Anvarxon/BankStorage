namespace BankStorage.Api.Interfaces
{
    public interface IImageUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
