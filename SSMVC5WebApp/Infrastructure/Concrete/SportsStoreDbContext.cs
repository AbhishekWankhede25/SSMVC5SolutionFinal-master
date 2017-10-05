using System.Data.Entity;

using SSMVC5WebApp.Infrastructure.Entities;

namespace SSMVC5WebApp.Infrastructure.Concrete
{
    //Only When you have more than one EFConfiguration, specify which one 
    //[DbConfigurationType(typeof(SportsStoreEfConfiguration))]
    public class SportsStoreDbContext : DbContext
    {
        public SportsStoreDbContext() : base("name=SportsStoreDBConn") { }
        //public SportsStoreDbContext() : base("name=DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("SportsStoreSchema");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}