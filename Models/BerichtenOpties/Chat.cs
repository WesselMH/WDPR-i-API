namespace BerichtenOpties;
using Accounts;

public class Chat
{
    public string Id { get; set; }
    public Account Verzender { get; set; }
    public Account Ontvanger { get; set; }
    public string Tekst { get; set; }
    public DateTime VerzendDatum { get; set; }
    public string? Opdracht { get; set; }
}