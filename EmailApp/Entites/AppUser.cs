using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailApp.Entites;

public class AppUser : IdentityUser<int> // Oluşacak entity'nin primary key'inin int olduğunu belirtiyoruz. Default olarak string'tir.
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Message> SentMessages { get; set; }
    public ICollection<Message> ReceivedMessages { get; set; }
}
