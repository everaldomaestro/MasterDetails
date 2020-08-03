using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    [Table(nameof(PrecoProduto))]
    public class PrecoProduto
    {
        //PROP
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(nameof(PrecoProdutoId))]
        public int PrecoProdutoId { get; set; }

        [Required(ErrorMessage ="Campo obrigatório"), Column(nameof(ProdutoId)), Display(Name = "Cliente")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(Preco), TypeName =("decimal(5,2)")), Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(InicioValidade), TypeName =("smalldatetime")), DataType(DataType.Date), Display(Name = "Início Validade")]
        public DateTime InicioValidade { get; set; }

        [Column(nameof(FinalValidade), TypeName = ("smalldatetime")), DataType(DataType.Date), Display(Name = "Final Validade")]
        public DateTime? FinalValidade { get; set; }

        //NAV
        [ForeignKey(nameof(ProdutoId))]
        public Produto Produto { get; set; }
    }
}