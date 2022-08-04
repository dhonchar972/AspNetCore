using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SimpleApp.Models;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!; // or // public DbSet<User> Users => Set<User>();
    public DbSet<Company>? Companies { get; set; } // can be null

    public ApplicationContext() // or // public ApplicationContext() => Database.EnsureCreated();
    {
        //similar to ddl - auto = create - drop, return boolean
        //DON'T USE TOGETHER WITH MIGRATIONS, IT DUPLICATE TABLES CREATION!!!!!
        Database.EnsureDeleted();
        Database.EnsureCreated();
        if (Database.CanConnect()) Console.WriteLine("Base is available!");
        else Console.WriteLine("Base is not available!!!");
    }

    //creating file logger
    readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);
    //closing stream
    public override void Dispose()
    {
        base.Dispose();
        logStream.Dispose();
    }
    //async closing stream
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await logStream.DisposeAsync();
    }

    //context configaration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(_connectionString);
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory.Split(@"bin\")[0])//so stupid thing
               .AddJsonFile("appsettings.json")
               .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);//accepts delegate
        //standart: Debug(uses by deafult), Trace, Debug, Information, Warning, Error, Critical, None
        //by category: Database.Command, Database.Connection, Database.Transaction, Migration, Model, Query, Scaffolding, Update, Infrastructure
        //optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), new[] { DbLoggerCategory.Database.Command.Name });
    }

    //Fluent API - gives additional options for configuring models in EF Core
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Age = 23 },
                new User { Id = 2, Name = "Alice", Age = 26 },
                new User { Id = 3, Name = "Sam", Age = 28 }
        );
    }
}

