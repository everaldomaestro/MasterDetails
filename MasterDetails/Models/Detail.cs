namespace MasterDetails.Models
{
    public class Detail
    {
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public int ProdutoId { get; set; }

        public Master Master { get; set; }
        public Produto Produto { get; set; }
    }
}