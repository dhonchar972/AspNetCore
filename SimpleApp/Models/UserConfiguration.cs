using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleApp.Models;

internal class UserConfiguration : IEntityTypeConfiguration<object>
{
    public void Configure(EntityTypeBuilder<object> builder)
    {
        //modelBuilder.Entity<Country>(); //add class Country to database
        //modelBuilder.Ignore<Company>();
        //modelBuilder.Entity<User>().Ignore(u => u.Company); //ignore field "Company" in User
        //modelBuilder.Entity<User>().Property("Id").HasField("id");
        //modelBuilder.Entity<User>().ToTable("People");
        //modelBuilder.Entity<User>().ToTable("People", schema: "userstore");
        //modelBuilder.Entity<User>().Property(b => b.Name).IsRequired();
        //modelBuilder.Entity<User>().HasKey(u => u.Ident);
        //modelBuilder.Entity<User>().HasKey(u => u.Ident).HasName("UsersPrimaryKey");
        //modelBuilder.Entity<User>().HasKey(u => new { u.PassportSeria, u.PassportNumber }); //Composite keys!!!
        //modelBuilder.Entity<User>().HasAlternateKey(u => u.Passport);
        //modelBuilder.Entity<User>().HasIndex(u => u.Passport);
        //modelBuilder.Entity<User>().Property(b => b.Id).ValueGeneratedNever();
        //modelBuilder.Entity<User>().Property(u => u.Age).HasDefaultValue(18);
        //modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("DATETIME('now')");
        //modelBuilder.Entity<User>().Property(u => u.Name).HasComputedColumnSql("FirstName || ' ' || LastName");
        //modelBuilder.Entity<User>().HasCheckConstraint("Age", "Age > 0 AND Age < 120", c => c.HasName("CK_User_Age"));
        //modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(50);
    }
}