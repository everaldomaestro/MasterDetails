using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    [Table(nameof(Produto))]
    public class Produto
    {
        //PROP
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(nameof(ProdutoId))]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(Nome), TypeName = ("varchar(25)")), StringLength(25)]
        public string Nome { get; set; }

        //NAV
        public IEnumerable<Detail> Details { get; set; }
        public IEnumerable<PrecoProduto> Precos { get; set; }
    }
}