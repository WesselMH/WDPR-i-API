using Microsoft.AspNetCore.Identity;

namespace Accounts;
public class Google : IdentityUser
{
    public int Id { get; set; }
    public string GebruikersNaam { get; set; }
    public string EmailGoogle { get; set; }
    public string sub { get; set; }
    public Account account { get; set; }
}