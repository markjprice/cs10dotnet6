using Microsoft.AspNetCore.SignalR.Client; // HubConnection
using Northwind.Chat.Models; // RegisterModel, MessageModel

using static System.Console;

Write("Enter a username: ");
string? username = ReadLine();

Write("Enter your groups: ");
string? groups = ReadLine();

HubConnection hubConnection = new HubConnectionBuilder()
  .WithUrl("https://localhost:5001/chat")
  .Build();

hubConnection.On<MessageModel>("ReceiveMessage", message =>
{
  WriteLine($"{message.From} says {message.Body} (sent to {message.To})");
});

await hubConnection.StartAsync();

WriteLine("Successfully started.");

RegisterModel registration = new()
{
  Username = username,
  Groups = groups
};

await hubConnection.InvokeAsync("Register", registration);

WriteLine("Successfully registered.");
WriteLine("Listening... (press ENTER to stop.)");
ReadLine();
