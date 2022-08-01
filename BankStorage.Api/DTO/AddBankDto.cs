namespace BankStorage.Api.DTO
{
    public class AddBankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
