using SimpleApp.Models;

namespace SimpleApp;
public class Program
{
    public static void Main(string[] args)
    {
        //adding
        using (ApplicationContext db = new())
        {
            User tom = new() { Name = "Tom", Age = 33 };
            User alice = new() { Name = "Alice", Age = 26 };

            db.Users.Add(tom); //await db.Users.AddRangeAsync(tom, alice);
            db.Users.Add(alice);// db.Users.AddRange(tom, alice);
            db.SaveChanges();//await db.SaveChangesAsync();
            Console.WriteLine("Saved successfully!");
        }

        //obtaining
        using (ApplicationContext db = new())
        {
            var users = db.Users.ToList();
            Console.WriteLine("List of objects:");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }

        //editing
        using (ApplicationContext db = new())
        {
            User? user = db.Users.FirstOrDefault();
            if (user != null)
            {
                user.Name = "Bob";
                user.Age = 44;
                //updating an object no longer tracked by data context 
                //db.Users.Update(user); //db.Users.UpdateRange(user1, users2);
                db.SaveChanges();
            }
            Console.WriteLine("\nData after editing:");
            var users = db.Users.ToList();
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }

        //deliting
        using (ApplicationContext db = new())
        {
            User? user = db.Users.FirstOrDefault();
            if (user != null)
            {
                db.Users.Remove(user);//db.Users.RemoveRange(user1, users2);
                db.SaveChanges();
            }
            Console.WriteLine("\nData after deliting:");
            var users = db.Users.ToList();
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }
    }
}