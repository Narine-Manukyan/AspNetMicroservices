using Npgsql;

namespace Discount.API.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        int retryForAvailability = retry ?? 0;
        using var scope = host.Services.CreateScope();
        
        var services = scope.ServiceProvider;
        var configuration =  services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();
        try
        {
            logger.LogInformation("Migrating postgresql database.");

            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = "Drop table if exists coupon";
            command.CommandText = "Drop table if exists coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"create table Coupon(Id serial primary key,
                                                        ProductName varchar(24) not null,
                                                        Description text,
                                                        Amount int)";
            command.ExecuteNonQuery();

            command.CommandText = @"insert into Coupon(ProductName, Description, Amount) 
                                    values
                                          ('IPhone X', 'IPhone Discount', 150),
                                          ('Samsung 10', 'Samsung Discount', 100)";
            command.ExecuteNonQuery();

            logger.LogInformation("Migrated postgresql database.");
        }
        catch (NpgsqlException ex)
        {
            logger.LogError(ex, "An error occured while migrating the postgresql database");

            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, retryForAvailability);
            }
        }

        return host;
    }
}
