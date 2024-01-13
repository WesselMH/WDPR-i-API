
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

    public List<Hulpmiddel>? HulpmiddelenLijst { get; set; }
    public List<Onderzoek>? OnderzoekenLijst { get; set; }
    public List<BeperkingOptie>? BeperkingenLijst { get; set; }
    public List<Categorie>? TypeOnderzoekenLijst { get; set; }
    public List<BenaderOptie>? BenaderOpties { get; set; }
    public List<Beschikbaarheid>? Beschikbaarheiden { get; set; }
}