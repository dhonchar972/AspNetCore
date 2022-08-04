using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleAppTwo.Models.Entities;

//[Owned] with it u don't need to add foreign key and property on main antity
public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [ForeignKey("CompanyInfoKey")]
    public int CompanyId { get; set; }// foreign key
    public Company? Company { get; set; }// navigation property, is dependent entity
}
