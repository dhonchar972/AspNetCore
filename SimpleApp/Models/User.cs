using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleApp.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    //one to one binding
    public Company? Company { get; set; }
}
