using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleApp.Models;

//[NotMapped]
//[Index("Name")]
//[Index("PhoneNumber", IsUnique = true, Name ="Phone_Index")]
//[Index("PhoneNumber", "Passport")]
[Table("COMPANY")]
public class Company
{
    [Key]
    [Column("ID")] //anotations, can give more configuration options
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]// == NOT NULL!!!
    [MaxLength(50)]
    public string? Name { get; set; }
    //dont need it for this binding!!!
    //public List<User>? Users { get; set; }
}
