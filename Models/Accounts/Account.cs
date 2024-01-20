using Microsoft.AspNetCore.Identity;

namespace Accounts;
public class Account : IdentityUser
{
    public string Id { get; set; }
    //is nodig voor het inloggen 
    public string GebruikersNaam { get; set; }
    public string Wachtwoord { get; set; }

    //om een of andere reden is email bij mij hele tijd null. even iets op verzinnen
    public string? EmailAccount { get; set; }
}