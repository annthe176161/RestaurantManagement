using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RestaurantManagement.EmployeeService.EntityFrameworkCore;

public class EmployeeServiceDbContextFactory : IDesignTimeDbContextFactory<EmployeeServiceDbContext>
{
    public EmployeeServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' not found. Configure it in appsettings.Development.json or ConnectionStrings__Default.");

        var builder = new DbContextOptionsBuilder<EmployeeServiceDbContext>()
            .UseSqlServer(connectionString);

        return new EmployeeServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
