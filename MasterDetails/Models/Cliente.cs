using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterDetails.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        public IEnumerable<Master> Masters { get; set; }
    }
}
