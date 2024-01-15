namespace BerichtenOpties;
using Accounts;

public class Email
{
    public string Id { get; set; }
    public Account Verzender { get; set; }
    public Account Ontvanger { get; set; }
    public string Tekst { get; set; }
    public DateTime VerzendDatum { get; set; }
    public string Status { get; set; }
}