using Accounts;

namespace Onderzoeken;

public class Onderzoek
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Locatie { get; set; }
    public string Status { get; set; }
    public string Beloning { get; set; }
    public DateTime Datum { get; set; }
    public Bedrijf Uitvoerder { get; set; }
    public Categorie SoortOnderzoek { get; set; }

    //dit zou toch een lijst moeten zijn omdat we meer dan 1 selectie criteria kunnen hebben? en nullable omdat en onderzoeken zullen zijn zonder specificatie
    public List<SelectieCriterium>? SelectieCriterium { get; set; }



}