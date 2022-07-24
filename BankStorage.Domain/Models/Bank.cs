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
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public List<Bin_Code> Bins { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bin_Code> Banks { get; set; }
    }
}
