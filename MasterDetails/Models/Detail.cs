using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    [Table(nameof(Detail))]
    public class Detail
    {
        //PROP
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(nameof(DetailId))]
        public int DetailId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(MasterId))]
        public int MasterId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(ProdutoId)), Display(Name ="Produto")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(Qtd)), Display(Name = "Quantidade")]
        public string Qtd { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Display(Name = "Preço"), NotMapped]
        public string PrecoFormatado { get; set; }

        [Required, Column(nameof(Preco), TypeName = ("decimal(5,2)"))]
        public decimal Preco { get; set; }

        //NAV
        [ForeignKey(nameof(MasterId))]
        public Master Master { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        public Produto Produto { get; set; }
    }
}