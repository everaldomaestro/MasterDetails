using System.ComponentModel.DataAnnotations;

namespace MasterDetails.Models
{
    public class Detail
    {
        public int DetailId { get; set; }
        public int MasterId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public string Qtd { get; set; }

        public Master Master { get; set; }
        public Produto Produto { get; set; }
    }
}