
namespace Accounts;

public class ErvaringsDeskundige : Account
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    // public override string GebruikersNaam
    // {
    //     get => GebruikersNaam;
    //     set { GebruikersNaam = Voornaam + " " + Achternaam; }
    // }
    public DateTime GeboorteDatum { get; set; }
    public string PostCode { get; set; }
    public string TelefoonNummer { get; set; }
    public Voogd? Voogd { get; set; }
}