using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleAppTwo.Models.Context;
using SimpleAppTwo.Models.Entities;

namespace SimpleAppTwo;

public class Program
{
    static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            Company company1 = new Company { Name = "Google" };
            Company company2 = new Company { Name = "Microsoft" };
            User user1 = new User { Name = "Jim", Company = company1 };
            User user2 = new User { Name = "Bin", Company = company2 };
            User user3 = new User { Name = "John", Company = company2 };

            db.Companies.AddRange(company1, company2);
            db.Users.AddRange(user1, user2, user3);
            db.SaveChanges();

            //Company company1 = new Company { Name = "Google" };
            //Company company2 = new Company { Name = "Microsoft" };
            //db.Companies.AddRange(company1, company2);  // добавление компаний
            //db.SaveChanges();

            //User user1 = new User { Name = "Tom", CompanyId = company1.Id };
            //User user2 = new User { Name = "Bob", CompanyId = company1.Id };
            //User user3 = new User { Name = "Sam", CompanyId = company2.Id };

            //db.Users.AddRange(user1, user2, user3);     // добавление пользователей
            //db.SaveChanges();


            // OR //


            //User user1 = new User { Name = "Tom" };
            //User user2 = new User { Name = "Bob" };
            //User user3 = new User { Name = "Sam" };

            //Company company1 = new Company { Name = "Google", Users = { user1, user2 } };
            //Company company2 = new Company { Name = "Microsoft", Users = { user3 } };

            //db.Companies.AddRange(company1, company2);  // добавление компаний
            //db.Users.AddRange(user1, user2, user3);     // добавление пользователей
            //db.SaveChanges();


            foreach (var user in db.Users.ToList())
            {
                Console.WriteLine($"Name: {user.Name}, Company: {user.Company?.Name}");
            }
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            //LINQ to Entities
            var users = (from user in db.Users.Include(p => p.Company)
                         where user.CompanyId == 1
                         select user).ToList();

            //LINQ to Entities
            var usersTwo = db.Users
                .Include(p => p.Company)
                .Where(p => p.CompanyId == 1);

            foreach (var user in users)
                Console.WriteLine($"{user.Name} - {user.Company?.Name}");
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            //RAW SQL!!!!
            var comps = db.Companies.FromSqlRaw("SELECT * FROM Companies").OrderBy(x => x.Name).ToList();
            foreach (var company in comps)
                Console.WriteLine(company.Name);
        }
        using (ApplicationContext db = new ApplicationContext())
        {
            //RAW SQL!!!!
            SqlParameter param = new SqlParameter("@name", "%Tom%");
            var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE @name", param).ToList();
            foreach (var user in users)
                Console.WriteLine(user.Name);

            // вставка
            string newComp = "Apple";
            int numberOfRowInserted = db.Database.ExecuteSqlRaw("INSERT INTO Companies (Name) VALUES ({0})", newComp);
            // асинхронная версия
            // int numberOfRowInserted2 = await db.Database.ExecuteSqlRawAsync("INSERT INTO Companies (Name) VALUES ({0})", newComp);


            // обновление
            string appleInc = "Apple Inc.";
            string apple = "Apple";
            int numberOfRowUpdated = db.Database.ExecuteSqlRaw("UPDATE Companies SET Name={0} WHERE Name={1}", appleInc, apple);

            // удаление
            int numberOfRowDeleted = db.Database.ExecuteSqlRaw("DELETE FROM Companies WHERE Name={0}", appleInc);
        }
        using (ApplicationContext db = new ApplicationContext())
        {
            //Interpolated raw SQL!!!!
            var name = "%Tom%";
            var age = 30;
            var users = db.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Name LIKE {name} AND Age > {age}").ToList();
            foreach (var user in users)
                Console.WriteLine(user.Name);
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            //Stored function!!!
            SqlParameter param = new SqlParameter("@name", "Bob");
            var users = db.Users.FromSqlRaw("SELECT * FROM GetUsersByAge (@name)", param).ToList();
            foreach (var u in users)
                Console.WriteLine($"{u.Name}");
        }
        using (ApplicationContext db = new ApplicationContext())
        {
            //Stored procedure!!!
            SqlParameter param = new("@name", "Microsoft");
            var users = db.Users.FromSqlRaw("GetUsersByCompany @name", param).ToList();
            foreach (var p in users)
                Console.WriteLine($"{p.Name}");
        }
    }
}