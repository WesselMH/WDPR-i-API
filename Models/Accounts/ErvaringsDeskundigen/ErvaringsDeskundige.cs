
using Onderzoeken;

namespace Accounts;

public class ErvaringsDeskundige : Account
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public DateTime GeboorteDatum { get; set; }
    public string PostCode { get; set; }
    public string TelefoonNummer { get; set; }
    public Voogd? Voogd { get; set; }

    public List<Hulpmiddel>? Hulpmiddelen { get; set; }
    public List<Onderzoek>? Onderzoeken { get; set; }
    public List<BeperkingOptie>? Beperkingen { get; set; }
    public List<Categorie>? TypeOnderzoeken { get; set; }
    public List<BenaderOptie>? BenaderOpties { get; set; }
    public List<Beschikbaarheid>? Beschikbaarheden { get; set; }
}