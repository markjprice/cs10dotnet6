namespace Northwind.Chat.Models;

public class MessageModel
{
  public string? To { get; set; }
  public string? ToType { get; set; }
  public string? From { get; set; }
  public string? Body { get; set; }
}
