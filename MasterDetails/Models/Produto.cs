using System.Collections.Generic;

namespace MasterDetails.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Detail> Details { get; set; }
    }
}