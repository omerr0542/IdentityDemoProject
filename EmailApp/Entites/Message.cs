using System.ComponentModel.DataAnnotations.Schema;

namespace EmailApp.Entites;

public class Message
{
    public int MessageId { get; set; }
    public int ReceiverId { get; set; } // Bunları tanımlama sebebimiz giriş yapan kullanıcının inbox ve outbox'ına girdiği zaman direkt olarak bu field'lar üzerinden sorgulamak
    public int SenderId { get; set; } 
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTime SendDate { get; set; }
    public AppUser Receiver { get; set; }
    public AppUser Sender { get; set; }
}
