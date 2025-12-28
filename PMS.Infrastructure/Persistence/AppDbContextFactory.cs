using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PMS.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<PMSContext>
    {
        public PMSContext CreateDbContext(string[] args)
        {
             // Get the base path of the Infrastructure project
            var basePath = Directory.GetCurrentDirectory();
              // Load configuration from appsettings.json (if exists in Infrastructure)
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            // Try to get connection string from config or fallback to default
            var connectionString = config.GetConnectionString("DefaultConnection")
                         ?? Environment.GetEnvironmentVariable("DefaultConnection")
                         ?? "Server=.;Database=HotelPMS_DB;Trusted_Connection=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<PMSContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PMSContext(optionsBuilder.Options);
        }
    }

}
