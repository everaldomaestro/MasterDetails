using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    [Table(nameof(Cliente))]
    public class Cliente
    {
        //PROP
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(nameof(ClienteId))]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(Nome), TypeName = ("varchar(25)")), StringLength(25)]
        public string Nome { get; set; }

        //NAV
        public IEnumerable<Master> Masters { get; set; }
    }
}
