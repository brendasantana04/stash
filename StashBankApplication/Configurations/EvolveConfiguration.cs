using EvolveDb;
using Microsoft.Data.SqlClient;

namespace StashBankApplication.Configurations
{
    public static class EvolveConfiguration
    {
        public static IServiceCollection AddEvolveConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                var connectionString = configuration.GetConnectionString("MSSQLServerSQLConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException("connection string com erro");
                }

                try
                {
                    using var evolveConnection = new SqlConnection(connectionString);
                    var evolve = new Evolve(evolveConnection, msg => Console.WriteLine(msg))
                        {
                            Locations = new List<string> { "db/migrations", "db/dataset" },
                            IsEraseDisabled = true
                        };
                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    throw new Exception("Um erro ocorreu ao tentar realizar a migration", ex);
                }
            }
            return services;
        }
    }
}
