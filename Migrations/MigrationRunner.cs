using ArkaynDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArkaynDAL.Migrations
{
    public class MigrationRunner
    {
        public async Task RunMigrationsAsync(
            IArkaynConnection connection,
            IEnumerable<IMigration> migrations)
        {
            // Sort migrations by Order
            var ordered = migrations.OrderBy(m => m.Order);

            foreach (var migration in ordered)
            {
                await migration.ApplyAsync(connection);
            }
        }
    }

    public abstract class BaseMigration : IMigration
    {
        public int Order { get; protected set; }

        public abstract Task ApplyAsync(IArkaynConnection connection);

        protected async Task ExecuteAsync(
            IArkaynConnection conn, string sql)
        {
            await conn.ExecuteNonQueryAsync(sql);
        }
    }
}

