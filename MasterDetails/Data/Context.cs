using MasterDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDetails.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Master> Masters { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
