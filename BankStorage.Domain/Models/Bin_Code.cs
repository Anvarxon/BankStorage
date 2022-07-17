using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankStorage.Domain.Models
{
    public class Bin_Code
    {
        public int Id { get; set; }
        public string BinCode { get; set; } = String.Empty;
        public CardTypes CardType { get; set; }

        public int BankId { get; set; }
        
        [JsonIgnore]
        public Bank? Bank { get; set; }
    }
    public enum CardTypes
    {
        UzCard,
        Humo
    }
}
