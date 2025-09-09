using EmailApp.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int> 
    // IdentityDbContext, IdentityUser ve IdentityRole'ü içerir
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>().HasMany(message => message.SentMessages)
                                    .WithOne(s => s.Sender)
                                    .HasForeignKey(user => user.SenderId)
                                    .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AppUser>().HasMany(message => message.ReceivedMessages)
                                    .WithOne(r => r.Receiver)
                                    .HasForeignKey(user => user.ReceiverId)
                                    .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }
    // Yukarıda AppUser ve AppRole tanımlandığı için DbSet olarak eklemeye gerek yoktur
    public DbSet<Message> Messages { get; set; }
}
