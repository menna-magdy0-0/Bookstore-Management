using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public StoreContext(): base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=BookStore;User Id=MennaMagdy;Password=Menna5684@noone;TrustServerCertificate=True;");
        }
        public virtual DbSet<Book> Books { get; set; }
    }
}
