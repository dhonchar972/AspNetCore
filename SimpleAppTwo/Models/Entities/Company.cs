namespace SimpleAppTwo.Models.Entities;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<User> Users { get; set; } = new(); // navigation property, is main entity, one to many
    //public User? User { get; set; } one to one
    //List -><- List - many to many, withaut ID
}
