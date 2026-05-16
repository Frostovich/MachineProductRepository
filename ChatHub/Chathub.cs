using System.Diagnostics;
using Machine_Product_Service.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Machine_Product_Service.MessagesEntities;
using Machine_Product_Service.MessagesEntities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Machine_Product_Service.ChatHub;

[Authorize]
public class Chathub : Hub{
    private readonly ILogger<Chathub> _logger;
    private readonly DBcontext _dbcontext;
    public Chathub(ILogger<Chathub> logger, DBcontext dbcontext) {
        _logger = logger;
        _dbcontext = dbcontext;
    }

    public async Task SendMessage(string message, string targetUserName) {
        if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(targetUserName))
        {
            return;
        }
        string senderId = Context.User?.Identity?.Name??"User";
        if (message.Length > 200) throw new HubException("You can write only 200 letters" );
        var chatMessages = new Message
        {
            message = message,
           MessageId = Guid.NewGuid().ToString(),
           CreatedOn = DateTime.UtcNow, 
           ReceiverName = targetUserName,
           IsRead = true
        };
        try
        {
            _dbcontext.Set<Message>().Add(chatMessages);
            _dbcontext.SaveChanges();
            
            string sendId = Context.User?.Identity?.Name ?? "User";

            await Clients.User(targetUserName).SendAsync("ReceivePrivateMessage", sendId, message);
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong");
        }
        
       


    }
    
}


