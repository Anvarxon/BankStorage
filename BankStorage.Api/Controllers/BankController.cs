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

            return Ok($"Банк с ID {id} удален");
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

            return Ok($"BIN код с ID {id} удален из банка с ID {bankId}");
        }

    }
}
