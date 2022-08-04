using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleAppTwo.Models.Entities;

namespace SimpleAppTwo.Models.Context;

public class ApplicationContext : DbContext
{
    private readonly string _connectionString =
        @"Server=(localdb)\MSSQLLocalDB;Database=SimpleTwo;Trusted_Connection=True;MultipleActiveResultSets=true";

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;

    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Console.WriteLine("Deleted previous tables!");
        Database.EnsureCreated();
        Console.WriteLine("Created new tables!");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);

        //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

        //IConfigurationRoot configuration = new ConfigurationBuilder()
        //       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory.Split(@"bin\")[0])//so stupid thing
        //       .AddJsonFile("appsettings.json")
        //       .Build();
        //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());

        //modelBuilder.Entity<Company>().OwnsOne(u => u.Users);

        //modelBuilder.Entity<User>()
        //    .HasOne(u => u.Company)
        //    .WithMany(c => c.Users)
        //    .HasForeignKey(u => u.CompanyInfoKey)
        //    .HasPrincipalKey(c => c.Name);

        //modelBuilder
        //    .Entity<User>()
        //    .HasOne(u => u.Profile)
        //    .WithOne(p => p.User)
        //    .HasForeignKey<UserProfile>(p => p.UserKey);

        //modelBuilder.Entity<User>().HasData(
        //        new User { Id = 1, Name = "Tom", Age = 23 },
        //        new User { Id = 2, Name = "Alice", Age = 26 },
        //        new User { Id = 3, Name = "Sam", Age = 28 }
        //);
    }
}
