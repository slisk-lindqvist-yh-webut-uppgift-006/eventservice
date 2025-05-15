using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Contexts;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        var connectionString = "Data Source=../Data/Databases/events_database.db;Cache=Shared";

        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite(connectionString);

        return new DataContext(optionsBuilder.Options);
    }
}