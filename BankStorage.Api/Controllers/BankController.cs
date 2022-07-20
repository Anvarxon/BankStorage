using BankStorage.Api.DTO;
using BankStorage.Api.Interfaces;
using BankStorage.Domain;
using BankStorage.Domain.Models;
using BankStorage.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<Bin_Code> _binCodeValidator;
        private readonly IValidator<CardDto> _cardValidator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageUploadService imageUploadService;

        public BankController(IRepository<Bank, int> bankRepository, 
                              IRepository<Bin_Code, int> binCodeRepository,
                              IValidator<Bin_Code> binCodeValidator,
                              IValidator<CardDto> cardValidator,
                              IWebHostEnvironment webHostEnvironment,
                              IImageUploadService imageUploadService)
        {
            this.bankRepository = bankRepository;
            this.binCodeRepository = binCodeRepository;
            this._binCodeValidator = binCodeValidator;
            this._cardValidator = cardValidator;
            this._webHostEnvironment = webHostEnvironment;
            this.imageUploadService = imageUploadService;
        }
        
        
        // BIN methods

        // 4.Добавление BIN кода к выбранному банку
        [HttpPost]
        [Route("addBinCode")]
        public async Task<IActionResult> AddBinCode(Bin_Code binCode)
        {
            ValidationResult result = await _binCodeValidator.ValidateAsync(binCode);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            
            binCodeRepository.Add(binCode);
            return Ok(binCode);
        }

        // 8.Удаление BIN кода из банка
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


        // BANK methods

        //3.добавление банка + загрузка логотипа и сохранение на диск. Размер и разрешение изображения нужно проверять;
        [HttpPost]
        [Route("addBank")]
        public async Task<IActionResult> AddBank(Bank bank)
        {
            try
            {
                await imageUploadService.UploadFile();
                
                bankRepository.Add(bank);
                await bankRepository.Save();
                return Ok(bank);
            }
            catch
            {
                return BadRequest();
            }
        }

        // 7.Удаление выбранного банка
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
        
        
    }
}