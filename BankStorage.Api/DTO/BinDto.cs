using BankStorage.Domain.Models;
using System.Collections.Generic;

namespace BankStorage.Api.DTO
{
    internal class BinDto
    {
        public string Id { get; set; }
        public string Bank { get; set; } = String.Empty;
        public List<Bin_Code> BinCodes { get; set; } = new List<Bin_Code>();

    }
}