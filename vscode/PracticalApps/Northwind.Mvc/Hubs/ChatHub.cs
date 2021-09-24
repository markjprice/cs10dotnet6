using Microsoft.AspNetCore.SignalR; // Hub
using Northwind.Chat.Models; // RegisterModel, MessageModel

namespace Northwind.Mvc.Hubs;

public class ChatHub : Hub
{
  // a new instance of ChatHub is created to process each method so
  // we must store usernames and their connectionids in a static field
  private static Dictionary<string, string> users = new();

  public async Task Register(RegisterModel model)
  {
    // add to or update dictionary with username and its connectionId
    users[model.Username] = Context.ConnectionId;

    foreach (string group in model.Groups.Split(','))
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }
  }

  public async Task SendMessage(MessageModel command)
  {
    MessageModel reply = new()
    {
      From = command.From,
      Body = command.Body
    };

    IClientProxy proxy;

    switch (command.ToType)
    {
      case "User":
        string connectionId = users[command.To];
        reply.To = $"{command.To} [{connectionId}]";
        proxy = Clients.Client(connectionId);
        break;

      case "Group":
        reply.To = $"Group: {command.To}";
        proxy = Clients.Group(command.To);
        break;

      default:
        reply.To = "Everyone";
        proxy = Clients.All;
        break;
    }

    await proxy.SendAsync(
      method: "ReceiveMessage", arg1: reply);
  }
}
