using BankStorage.Api.DTO;
using BankStorage.Api.Interfaces;
using BankStorage.Domain;
using BankStorage.Domain.Models;
using BankStorage.Infrastructure.Context;
using BankStorage.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IValidator<Bank> _bankValidator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageUploadService imageUploadService;
        private readonly AppDbContext _context;

        public BankController(IRepository<Bank, int> bankRepository, 
                              IRepository<Bin_Code, int> binCodeRepository,
                              IValidator<Bin_Code> binCodeValidator,
                              IValidator<CardDto> cardValidator,
                              IValidator<Bank> bankValidator,
                              IWebHostEnvironment webHostEnvironment,
                              IImageUploadService imageUploadService)
        {
            this.bankRepository = bankRepository;
            this.binCodeRepository = binCodeRepository;
            this._binCodeValidator = binCodeValidator;
            this._cardValidator = cardValidator;
            this._bankValidator = bankValidator;
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

            await binCodeRepository.Add(binCode);
            return Ok(binCode);
        }

        // 6.редактирование BIN кода
        [HttpPut]
        [Route("editBinCode")]
        public async Task<IActionResult> EditBinCode(Bin_Code binCode)
        {
            ValidationResult result = await _binCodeValidator.ValidateAsync(binCode);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await binCodeRepository.Update(binCode);
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

        // Получить список всех банки и список их BIN с помощью метода .Include()
        [HttpGet]
        [Route("getAllBanksAndListOfBins")]
        public async Task<IActionResult> GetAllBanksAndListOfBins(Bank bank)
        {
            var banks = await bankRepository.GetAll();
            var bins = await binCodeRepository.GetAll();
            var banksWithBins = _context.Banks.Include(b => b.Bins);
            return Ok(banksWithBins);
        }


        // 3.добавление банка + загрузка логотипа и сохранение на диск. Размер и разрешение изображения нужно проверять;
        [HttpPost]
        [Route("addBank")]
        public async Task<IActionResult> AddBank(Bank bank)
        {
            ValidationResult result = await _bankValidator.ValidateAsync(bank);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await bankRepository.Add(bank);
            return Ok(bank);
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