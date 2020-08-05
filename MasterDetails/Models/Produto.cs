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

        [Required(ErrorMessage = "Campo obrigatório"), Display(Name = "Preço"), NotMapped]
        public string PrecoFormatado { get; set; }

        [Required, Column(nameof(Preco), TypeName = ("decimal(5,2)")), Display(Name = "Preço")]
        public decimal Preco { get; private set; }

        //NAV
        public IEnumerable<Detail> Details { get; set; }

        //ACTION
        public void DefinirPrecoFormatado()
        {
            Preco = decimal.Parse(PrecoFormatado);
        }

        public void ObterPrecoFormatado()
        {
            PrecoFormatado = Preco.ToString();
        }
    }
}