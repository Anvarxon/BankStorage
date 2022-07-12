using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankStorage.Domain.Models
{
    public class Bank
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bank name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Logo is required")]
        public byte[] Logo { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Bin_Code> Banks { get; set; }
    }
}
