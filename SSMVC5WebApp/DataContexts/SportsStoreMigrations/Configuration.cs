namespace SSMVC5WebApp.DataContexts.SportsStoreMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SSMVC5WebApp.Infrastructure.Concrete.SportsStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\SportsStoreMigrations";
        }

        protected override void Seed(SSMVC5WebApp.Infrastructure.Concrete.SportsStoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //context.Products.AddOrUpdate(p=>p.ProductName,
            //    new Product { ProductName = "Soccer Ball", Price = 100.29m, Description = "FIFA-approved size and weight", Category = "soccer" });

            #region Default
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            // 
            #endregion
        }
    }
}
