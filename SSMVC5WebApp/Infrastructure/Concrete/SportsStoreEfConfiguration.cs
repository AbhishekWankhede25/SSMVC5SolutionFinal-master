using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace SSMVC5WebApp.Infrastructure.Concrete
{
    public class SportsStoreEfConfiguration : DbConfiguration
    {
        public SportsStoreEfConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}