using BankStorage.Domain;
using BankStorage.Domain.Models;
using BankStorage.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BankStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IRepository<Bank, int> bankRepository;
        private readonly IRepository<Bin_Code, int> binCodeRepository;
        
        public BankController(IRepository<Bank, int> bankRepository, 
                              IRepository<Bin_Code, int> binCodeRepository)
        {
            this.bankRepository = bankRepository;
            this.binCodeRepository = binCodeRepository;
        }

        // Добавление BIN кода к выбранному банку
        [HttpPost]
        [Route("addBinCode")]
        public async Task<IActionResult> AddBinCode(Bin_Code binCode)
        {
            if (binCode.BankId == 0)
            {
                return BadRequest("Не указан банк");
            }
            if (binCode.BinCode == null)
            {
                return BadRequest("Не указан код");
            }
            if (binCode.BinCode.Length > 6)
            {
                return BadRequest("Код не должен состоять больше 6 цифр");
            }
            await binCodeRepository.Add(binCode);
            return Ok(binCode);
        }

        // Удаление выбранного банка
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var bank = await bankRepository.GetById(id);
            if (bank == null)
            {
                return NotFound();
            }

            await bankRepository.Delete(id);
            await bankRepository.Save();

            return Ok($"Bank with the ID {id} has been deleted");
        }

        // Удаление BIN кода из банка
        [HttpDelete("BinId/{id}/bankId/{bankId}")]
        public async Task<IActionResult> DeleteBinCode(int id, int bankId)
        {
            var binCode = await binCodeRepository.GetById(id);
            if (binCode == null)
            {
                return NotFound();
            }

            await binCodeRepository.Delete(id);
            await binCodeRepository.Save();

            return Ok($"Bin code with the ID {id} from Bank with the ID {bankId} has been deleted");
        }

    }
}
