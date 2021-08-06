using Microsoft.EntityFrameworkCore;

namespace MyFirstRest.Model
{
    public class StoreContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartPosition> ShoppingCartPositions { get; set; }
        
        public DbSet<BillHead> BillHeads { get; set; }
        public DbSet<BillPosition> BillPosition { get; set; }
        
        protected StoreContext()
        {
        }

        public StoreContext(DbContextOptions options) : base(options)
        {
        }
    }
}