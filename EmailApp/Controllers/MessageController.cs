using EmailApp.Context;
using EmailApp.Entites;
using EmailApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace EmailApp.Controllers;

[Authorize]
public class MessageController(AppDbContext context, UserManager<AppUser> userManager) : Controller
{
    public async Task<IActionResult> Inbox()
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);

        var messages = context.Messages
            .Where(m => m.ReceiverId == user.Id)
            .Include(m => m.Sender)
            .OrderByDescending(m => m.SendDate)
            .ToList();

        return View(messages);
    }

    public async Task<IActionResult> Outbox()
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);

        var messages = context.Messages
            .Where(m => m.SenderId == user.Id)
            .Include(m => m.Receiver)
            .OrderByDescending(m => m.SendDate)
            .ToList();
        return View(messages);
    }

    public IActionResult Detail(int id)
    {
        //var message = context.Messages.Include(m => m.Sender).Find(id);

        var message = context.Messages
            .Include(m => m.Sender)
            .FirstOrDefault(m => m.MessageId == id);

        return View(message); 
    }

    [HttpGet]
    public IActionResult SendMessage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageViewModel model)
    {
        var sender = await userManager.FindByNameAsync(User.Identity.Name);
        var receiver = await userManager.FindByEmailAsync(model.ReceiverEmail);

        if (model.ReceiverEmail == null || receiver == null)
        {
            ModelState.AddModelError("", "Alıcı bulunamadı.");
            return View(model);
        }
        if(model.Subject == null)
        {
            ModelState.AddModelError("", "Konu boş olamaz.");
            return View(model);
        }

        if (model.Body == null)
        {
            ModelState.AddModelError("", "Mesaj boş olamaz.");
            return View(model);
        }

        if(sender.Id == receiver.Id)
        {
            ModelState.AddModelError("", "Kendinize Mesaj Gönderemezsiniz");
            return View(model);
        }

        var message = new Message
        {
            Subject = model.Subject,
            Body = model.Body,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            SendDate = DateTime.Now
        };

        context.Messages.Add(message);
        context.SaveChanges();

        return RedirectToAction("Inbox");
    }
}
